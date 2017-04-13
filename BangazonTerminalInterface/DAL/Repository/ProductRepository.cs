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
using System.Globalization;

namespace BangazonTerminalInterface.DAL.Repository
{
    public class ProductRepository : IProduct
    {
        SqlConnection slytherBangConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);

        ConsoleHelper _consoleHelper;

        public ProductRepository()
        {
            _consoleHelper = new ConsoleHelper();
        }
        public class ProductPopularity
        {
            public string ProductName { get; set; }
            public int Orders { get; set; }
            public int Customers { get; set; }
            public decimal Revenue { get; set; }
        }

        public List<ProductPopularity> GetProductPopularity()
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

                var listedPopularity = new List<ProductPopularity>();
                var reader = getProductsCommand.ExecuteReader();
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
                }
                return listedPopularity;
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
            return new List<ProductPopularity>();
        }

        // replaced with function located above by TH 

        public List<Product> GetAllProducts()
        {
            slytherBangConnection.Open();

            try
            {
                var getProductCommand = slytherBangConnection.CreateCommand();
                getProductCommand.CommandText = @"
                    SELECT ProductId, ProductName, CONVERT(decimal(10,2),ProductPrice) as ProductPrice
                    FROM Product";

                var reader = getProductCommand.ExecuteReader();

                var products = new List<Product>();
                while (reader.Read())
                {
                    var product = new Product
                    {
                        ProductId = reader.GetInt32(0),
                        ProductName = reader.GetString(1),
                        ProductPrice = reader.GetDecimal(2)
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
            catch (Exception ex)
            {
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
