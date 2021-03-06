﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using BangazonTerminalInterface.Components;

namespace BangazonTerminalInterface.DAL.Repository
{
    class PaymentRepository
    {
        IDbConnection _sqlConnection;

        public PaymentRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);
        }


        public void AddPayment(int customerId, string paymentType, long accountNumber)

        {
            _sqlConnection.Open();
            try
            {
                var addPaymentCommand = _sqlConnection.CreateCommand();
                addPaymentCommand.CommandText = "INSERT into Payment(CustomerId, PaymentType, PaymentAccountNumber) values(@customerId, @paymentType, @accountNumber)";

                var customerParameter = new SqlParameter("customerId", SqlDbType.Int);
                customerParameter.Value = customerId;
                addPaymentCommand.Parameters.Add(customerParameter);

                var typeParameter = new SqlParameter("paymentType", SqlDbType.VarChar);
                typeParameter.Value = paymentType;
                addPaymentCommand.Parameters.Add(typeParameter);

                var accountParameter = new SqlParameter("accountNumber", SqlDbType.BigInt);
                accountParameter.Value = accountNumber;
                addPaymentCommand.Parameters.Add(accountParameter);

                addPaymentCommand.ExecuteNonQuery();


            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _sqlConnection.Close();
            } 
        }

        public List<Payment> GetAllPayments(int customerId)
        {
            _sqlConnection.Open();

            try
            {
                var getPaymentsCommand = _sqlConnection.CreateCommand();
                getPaymentsCommand.CommandText = @"
                    SELECT PaymentId, CustomerId, PaymentType, PaymentAccountNumber 
                    FROM Payment
                    WHERE CustomerId = @customerId";

                var customerParameter = new SqlParameter("customerId", SqlDbType.Int);
                customerParameter.Value = customerId;
                getPaymentsCommand.Parameters.Add(customerParameter);
                var reader = getPaymentsCommand.ExecuteReader();

                var payments = new List<Payment>();
                while (reader.Read())
                {
                    var payment = new Payment
                    {
                        PaymentId = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        PaymentType = reader.GetString(2),
                        PaymentAccountNumber = reader.GetInt64(3)
                    };

                    payments.Add(payment);
                }

                return payments;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return new List<Payment>();
        }

        public Payment GetPaymentById(int paymentId)
        {
            _sqlConnection.Open();

            try
            {
                var getPaymentCommand = _sqlConnection.CreateCommand();
                getPaymentCommand.CommandText = @"
                    SELECT PaymentId, CustomerId, PaymentType, PaymentAccountNumber 
                    FROM Payment 
                    WHERE PaymentId = @paymentId";
                var productIdParam = new SqlParameter("paymentId", SqlDbType.Int);
                productIdParam.Value = paymentId;
                getPaymentCommand.Parameters.Add(productIdParam);

                var reader = getPaymentCommand.ExecuteReader();

                if (reader.Read())
                {
                    var payment = new Payment
                    {
                        PaymentId = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        PaymentType = reader.GetString(2),
                        PaymentAccountNumber = reader.GetInt32(3)
                    };
                    return payment;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _sqlConnection.Close();
            }

            return null;
        }

    }
}
