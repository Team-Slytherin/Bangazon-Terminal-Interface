using BangazonTerminalInterface.Interfaces;
using System;
using System.Collections.Generic;
using BangazonTerminalInterface.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using BangazonTerminalInterface.Helpers;

namespace BangazonTerminalInterface.DAL.Repository
{
    public class ProductRepository : IProduct
    {
        SqlConnection slytherBangConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);

        private class ProductPopularity
        {
            public string ProductName { get; set; }
            public int Orders { get; set; }
            public int Customers { get; set; }
            public decimal Revenue { get; set; }
        }
        public void GetProductPopularity()
        {
            slytherBangConnection.Open();
            try
            {
                var getProductsCommand = slytherBangConnection.CreateCommand();
                getProductsCommand.CommandText =
              @"SELECT distinct Top 10
                  ProductName,
                  COUNT(distinct CartDetailId) as Orders,
                  COUNT(distinct CustomerId) as Customers,
                  CONVERT(decimal(10,2), SUM(Qty * ProductPrice)) as Revenue
                FROM SlytherBang.dbo.Cart c
                JOIN SlytherBang.dbo.CartDetail cc
                  ON c.CartId = cc.CartId
                JOIN SlytherBang.dbo.Product p
                  ON cc.ProductId = p.ProductId
                GROUP BY ProductName
                ORDER BY COUNT(distinct CartDetailId) desc; ";
                Console.Clear();
                Helper.WriteHeaderToConsole("Product Popularity Report");
                Console.WriteLine("Product           Orders     Customers  Revenue          ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*********************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                var listedPopularity = new List<ProductPopularity>();
                var reader = getProductsCommand.ExecuteReader();
                char spacePad = ' ';
                while (reader.Read())
                {
                    var product = new ProductPopularity
                    {
                        ProductName = reader.GetString(0),
                        Orders = reader.GetInt32(1),
                        Customers = reader.GetInt32(2),
                        Revenue = reader.GetDecimal(3)
                    };
                    listedPopularity.Add(product);
                    Console.WriteLine(product.ProductName.PadRight(18, spacePad).Substring(0, 17) + spacePad + product.Orders.ToString().PadRight(11, spacePad).Substring(0, 11) + product.Customers.ToString().PadRight(11, spacePad).Substring(0, 11) + "$" + product.Revenue);
                }
                decimal totalOrders = listedPopularity.Sum(item => item.Orders);
                decimal totalCustomers = listedPopularity.Sum(item => item.Customers);
                decimal totalRevenue = listedPopularity.Sum(item => item.Revenue);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("*********************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Totals:           " + totalOrders.ToString().PadRight(11, spacePad).Substring(0, 10) + spacePad + totalCustomers.ToString().PadRight(11, spacePad).Substring(0, 11) + "$" + totalRevenue.ToString());
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadKey();
            }
            catch (SqlException ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                slytherBangConnection.Close();
            }
        }

        // replaced with function located above by TH 

        public List<Product> GetAllProducts()
        {
            slytherBangConnection.Open();

            try
            {
                var getProductCommand = slytherBangConnection.CreateCommand();
                getProductCommand.CommandText = @"
                    SELECT ProductId, ProductName, ProductPrice 
                    FROM Product";

                var reader = getProductCommand.ExecuteReader();

                var products = new List<Product>();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        ProductPrice = reader.GetSqlMoney(2).ToDecimal()
                    };

                    products.Add(product);
                }

                return products;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                slytherBangConnection.Close();
            }

            return new List<Product>();
        }

        public Product GetProductById(int productId)
        {
            slytherBangConnection.Open();

            try
            {
                var getProductCommand = slytherBangConnection.CreateCommand();
                getProductCommand.CommandText = @"
                    SELECT ProductId, ProductName, ProductPrice 
                    FROM Product 
                    WHERE ProductId = @productId";
                var productIdParam = new SqlParameter("productId", SqlDbType.Int);
                productIdParam.Value = productId;
                getProductCommand.Parameters.Add(productIdParam);

                var reader = getProductCommand.ExecuteReader();

                if (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        ProductPrice = reader.GetSqlMoney(2).ToDecimal()
                    };
                    return product;
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            finally
            {
                slytherBangConnection.Close();
            }

            return null;
        }
    }
}
