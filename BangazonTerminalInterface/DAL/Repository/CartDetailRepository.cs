using BangazonTerminalInterface.Components;
using System;
using System.Collections;
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
    class CartDetailRepository
    {
        IDbConnection _bangzonConnection;

        public CartDetailRepository()
        {
            _bangzonConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);
        }

        public void AddProduct(int cartId, int productId, int qty)
        {
            _bangzonConnection.Open();

            try
            {
                var addProductCommand = _bangzonConnection.CreateCommand();
                addProductCommand.CommandText = @"insert into CartDetail(CartId, ProductId, Qty) values(@cartId, @productId, @qty)";
                var cartIdParameter = new SqlParameter("cartId", SqlDbType.Int);
                cartIdParameter.Value = cartId;
                addProductCommand.Parameters.Add(cartIdParameter);
                var productIdParameter = new SqlParameter("productId", SqlDbType.Int);
                productIdParameter.Value = productId;
                addProductCommand.Parameters.Add(productIdParameter);
                var qtyParameter = new SqlParameter("qty", SqlDbType.Int);
                qtyParameter.Value = qty;
                addProductCommand.Parameters.Add(qtyParameter);

                addProductCommand.ExecuteNonQuery();
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

        public decimal GetCartPrice(int cartId)
        {
            decimal totalPrice = 0;

            _bangzonConnection.Open();

            try
            {
                var getCartDetailCommand = _bangzonConnection.CreateCommand();
                getCartDetailCommand.CommandText = @"SELECT Qty, ProductPrice
                                                    FROM (SELECT * FROM CartDetail WHERE CartDetail.CartId = @cartId) a
                                                        JOIN Product ON a.ProductId = Product.ProductId";
                var cartIdParameter = new SqlParameter("cartId", SqlDbType.Int);
                cartIdParameter.Value = cartId;
                getCartDetailCommand.Parameters.Add(cartIdParameter);
                var reader = getCartDetailCommand.ExecuteReader();

                while (reader.Read())
                {
                    totalPrice = totalPrice + reader.GetInt32(0) * reader.GetDecimal(1);
                }
                return totalPrice;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                _bangzonConnection.Close();
            }
            return totalPrice;
        }
    }
}
