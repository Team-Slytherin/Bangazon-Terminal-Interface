using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace BangazonTerminalInterface.DAL.Repository
{
    class PaymentRepository
    {
        IDbConnection _sqlConnection;

        public PaymentRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);
        }

        public void AddPayment(string paymentType, int accountNumber)
        {
            _sqlConnection.Open();
            try
            {
                var addPaymentCommand = _sqlConnection.CreateCommand();
                addPaymentCommand.CommandText = "INSERT into Payment(PaymentType, PaymentAccountNumber) values(@paymentType,@accountNumber)";
                var typeParameter = new SqlParameter("paymentType", SqlDbType.VarChar);
                typeParameter.Value = paymentType;
                addPaymentCommand.Parameters.Add(typeParameter);
                var accountParameter = new SqlParameter("accountNumber", SqlDbType.Int);
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
    }
}
