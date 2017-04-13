using BangazonTerminalInterface.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
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

        public string GetCartPrice(int cartId)
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
                return totalPrice.ToString("C", CultureInfo.CurrentCulture);

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
            return totalPrice.ToString("C", CultureInfo.CurrentCulture); ;
        }

        public int GetTotalItemsInCart(int cartId)
        {
            _bangzonConnection.Open();

            try
            {
                var getCartDetailCommand = _bangzonConnection.CreateCommand();
                getCartDetailCommand.CommandText = @"SELECT Sum(Qty)
                                                    FROM (SELECT * FROM CartDetail WHERE CartDetail.CartId = @cartId) a
                                                        JOIN Product ON a.ProductId = Product.ProductId";
                var cartIdParameter = new SqlParameter("cartId", SqlDbType.Int);
                cartIdParameter.Value = cartId;
                getCartDetailCommand.Parameters.Add(cartIdParameter);

                var reader = getCartDetailCommand.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count += reader.GetInt32(0);
                }
                return count;

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
            return 0;
        }

        public List<CartItem> GetItemsInCart(int customerId)
        {
            _bangzonConnection.Open();

            try
            {
                var getCartDetailCommand = _bangzonConnection.CreateCommand();
                getCartDetailCommand.CommandText = @"
                    SELECT DISTINCT ProductName, ProductPrice, count(distinct CartDetailId) as Qty, ProductPrice*count(distinct CartDetailId) as Total
                    FROM SlytherBang.dbo.Customer cust
                    JOIN SlytherBang.dbo.Cart cart
                     ON cust.CustomerId = cart.CustomerId
                    JOIN SlytherBang.dbo.CartDetail cdt
                     ON cart.CartId = cdt.CartId
                    JOIN SlytherBang.dbo.Product p
                     ON cdt.ProductId = p.ProductId
                    WHERE Active = '1'
                    AND cust.CustomerId = @customerId
                    GROUP BY ProductName, ProductPrice;";
                var customerIdParameter = new SqlParameter("customerId", SqlDbType.Int);
                customerIdParameter.Value = customerId;
                getCartDetailCommand.Parameters.Add(customerIdParameter);
                var cartItems = new List<CartItem>();
                var reader = getCartDetailCommand.ExecuteReader();
                while (reader.Read())
                {
                    var cartItem = new CartItem
                    {
                        ProductName = reader.GetString(0),
                        ProductPrice = reader.GetDecimal(1),
                        ProductQuantity = reader.GetInt32(2),
                        Total = reader.GetDecimal(3)
                        
                    };

                    cartItems.Add(cartItem);
                }

                return cartItems;

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
            return new List<CartItem>();
        }
    }

    public class CartItem
    {
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public decimal Total { get; set; }
    }
}
