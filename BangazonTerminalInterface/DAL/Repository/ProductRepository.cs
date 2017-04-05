using BangazonTerminalInterface.Interfaces;
using System;
using System.Collections.Generic;
using BangazonTerminalInterface.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Diagnostics;

namespace BangazonTerminalInterface.DAL.Repository
{
    public class ProductRepository : IProduct
    {
        SqlConnection slytherBangConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);

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
            catch (Exception ex) {
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
