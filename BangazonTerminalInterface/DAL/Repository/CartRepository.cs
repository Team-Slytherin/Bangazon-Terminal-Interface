using BangazonTerminalInterface.Components;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DAL.Repository
{
    class CartRepository
    {
        IDbConnection _bangzonConnection;

        public CartRepository()
        {
            _bangzonConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);
        }

        public void AddCart(int customerId)
        {
            _bangzonConnection.Open();

            try
            {
                var addCartCommand = _bangzonConnection.CreateCommand();
                addCartCommand.CommandText = @"insert into Cart(CustomerId, Active) values(@customerId, true)";
                var customerIdParameter = new SqlParameter("customerId", SqlDbType.Int);
                customerIdParameter.Value = customerId;
                addCartCommand.Parameters.Add(customerIdParameter);

                addCartCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _bangzonConnection.Close();
            }
        }

        public Cart GetActiveCart(int customerId)
        {
            _bangzonConnection.Open();

            try
            {
                var getActiveCartCommand = _bangzonConnection.CreateCommand();
                getActiveCartCommand.CommandText = @"SELECT CartId, CustomerId, PaymentId, Active 
                                                FROM Cart 
                                                WHERE CustomerId = @customerId AND Active = true";
                var customerIdParameter = new SqlParameter("customerId", SqlDbType.Int);
                customerIdParameter.Value = customerId;
                getActiveCartCommand.Parameters.Add(customerIdParameter);
                var reader = getActiveCartCommand.ExecuteReader();

                while (reader.Read())
                {
                    reader.Read();

                    var Cart = new Cart()
                    {
                        CartId = reader.GetInt32(0),
                        CustomerId = reader.GetInt32(1),
                        PaymentId = reader.GetInt32(2),
                        Active = reader.GetBoolean(3)
                    };
                    return Cart;
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                _bangzonConnection.Close();
            }
            return null;
        }

        public void EditCartStatus(int cartId, int paymentId)
        {
            _bangzonConnection.Open();

            try
            {
                var editCartCommand = _bangzonConnection.CreateCommand();
                editCartCommand.CommandText = @"UPDATE Cart
                                                SET PaymentId = @paymentId, Active = false
                                                WHERE CartId = @cartId";
                var cartIdParameter = new SqlParameter("cartId", SqlDbType.Int);
                cartIdParameter.Value = cartId;
                editCartCommand.Parameters.Add(cartIdParameter);
                var paymentIdParameter = new SqlParameter("paymentId", SqlDbType.VarChar);
                paymentIdParameter.Value = paymentId;
                editCartCommand.Parameters.Add(paymentIdParameter);

                var rowsAffected = editCartCommand.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    throw new Exception("Query didn't work!");
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _bangzonConnection.Close();
            }

        }
    }
}
