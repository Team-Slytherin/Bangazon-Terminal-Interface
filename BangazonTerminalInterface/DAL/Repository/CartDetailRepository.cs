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
                addProductCommand.CommandText = @"insert into CartDetail(CartDetailId, CartId, ProductId, Qty) values(@cartDetailId, @cartId, @productId, @qty)";
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

        public float GetCartPrice(int cartId)
        {
            float totalPrice = 0;

            _bangzonConnection.Open();

            try
            {
                var getCartDetailCommand = _bangzonConnection.CreateCommand();
                getCartDetailCommand.CommandText = @"SELECT Qty, ProductPrice
                                                    FROM (SELECT * FROM CartDetail WHERE Cart.Cart.Id = @cartId
                                                        JOIN Product ON CartDetail.ProductId = Product.ProductId)";
                var cartIdParameter = new SqlParameter("cartId", SqlDbType.Int);
                cartIdParameter.Value = cartId;
                getCartDetailCommand.Parameters.Add(cartIdParameter);
                var reader = getCartDetailCommand.ExecuteReader();

                while (reader.Read())
                {
                    reader.Read();

                    totalPrice += reader.GetInt32(0) * reader.GetFloat(1);
                }
                return totalPrice;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                _bangzonConnection.Close();
            }
            return totalPrice;
        }

        public List<IList> GetProductPopularity()
        {
            List<IList> result = null;
            _bangzonConnection.Open();

            try
            {
                var getCartDetailCommand = _bangzonConnection.CreateCommand();
                getCartDetailCommand.CommandText = @"SELECT ProductName, COUNT(DISTINCT CartId) AS OrderCount, COUNT(DISTINCT CustomerId) AS CustomerCount, SUM(Qty * ProductPrice) AS Revenue 
                                                    FROM (CartDetail JOIN Product ON CartDetail.ProductId = Product.ProductId)
                                                    GROUP BY ProductName";
                var reader = getCartDetailCommand.ExecuteReader();

                while (reader.Read())
                {
                    reader.Read();

                    var itemSum = new List<Object>();
                    itemSum.Add(reader.GetInt32(0));
                    itemSum.Add(reader.GetInt32(1));
                    itemSum.Add(reader.GetInt32(2));
                    itemSum.Add(reader.GetInt32(3));
                    result.Add(itemSum);
                }
                return result;
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
