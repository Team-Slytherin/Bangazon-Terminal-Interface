using BangazonTerminalInterface.Components;
using System;
using System.Collections.Generic;
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

        public CartRepository(IDbConnection bangzonConnection)
        {
            _bangzonConnection = bangzonConnection;
        }

        public void AddCart(int cartId, int customerId, int paymentId, bool active)
        {
            _bangzonConnection.Open();

            try
            {
                var addCartCommand = _bangzonConnection.CreateCommand();
                addCartCommand.CommandText = @"insert into Cart(cartId, customerId, paymentId, active) values(@cartId, @customerId, @paymentId, @active)";
                var cartIdParameter = new SqlParameter("cartId", SqlDbType.Int);
                cartIdParameter.Value = cartId;
                addCartCommand.Parameters.Add(cartIdParameter);
                var customerIdParameter = new SqlParameter("customerId", SqlDbType.Int);
                customerIdParameter.Value = customerId;
                addCartCommand.Parameters.Add(customerIdParameter);
                var paymentIdParameter = new SqlParameter("paymentId", SqlDbType.Int);
                paymentIdParameter.Value = paymentId;
                addCartCommand.Parameters.Add(paymentIdParameter);
                var activeParameter = new SqlParameter("active", SqlDbType.Int);
                activeParameter.Value = active;
                addCartCommand.Parameters.Add(activeParameter);

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

        public Cart GetCart(int cartId)
        {
            _bangzonConnection.Open();

            try
            {
                var getCartCommand = _bangzonConnection.CreateCommand();
                getCartCommand.CommandText = @"SELECT CartId, CustomerId, PaymentId, Active 
                                                FROM Cart 
                                                WHERE CartId = @cartId";
                var cartIdParameter = new SqlParameter("cartId", SqlDbType.Int);
                cartIdParameter.Value = cartId;
                getCartCommand.Parameters.Add(cartIdParameter);
                var reader = getCartCommand.ExecuteReader();

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
    }
}
