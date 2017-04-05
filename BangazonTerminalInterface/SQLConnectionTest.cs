using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace BangazonTerminalInterface
{
    class SQLConnectionTest
    {
        IDbConnection _sqlConnection;

        public SQLConnectionTest ()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);
        }

        public void GetProducts()
        {
            _sqlConnection.Open();
            try
            {
                var getProductsCommand = _sqlConnection.CreateCommand();
                getProductsCommand.CommandText = @"SELECT COUNT(*) FROM SlytherBang.dbo.Product;";

                int count = (int) getProductsCommand.ExecuteScalar();
                Console.WriteLine("There are " + count + " product(s) in the Product table");
                Thread.Sleep(1500);
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
