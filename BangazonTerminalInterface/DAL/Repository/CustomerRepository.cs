using BangazonTerminalInterface.DataValidation.CustomerValidation;
using BangazonTerminalInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangazonTerminalInterface.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;

namespace BangazonTerminalInterface.DAL.Repository
{
    public class CustomerRepository : ICustomer
    {

        IDbConnection _sqlConnection;

        public CustomerRepository()
        {
            _sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["SlytherBangConnection"].ConnectionString);
        }

        public void AddCustomer(Customer customer)
        {
            _sqlConnection.Open();

            try
            {
                var addCustomerCommand = _sqlConnection.CreateCommand();
                addCustomerCommand.CommandText = @"
                INSERT INTO Customer(CustomerName,CustomerStreetAddress,CustomerCity,CustomerState,CustomerZip,CustomerPhone)
                VALUES(@name,@address,@city,@state,@zip,@phone)";
                var nameParameter = new SqlParameter("name", SqlDbType.VarChar);
                nameParameter.Value = customer.CustomerName;
                addCustomerCommand.Parameters.Add(nameParameter);
                var addressParameter = new SqlParameter("address", SqlDbType.VarChar);
                addressParameter.Value = customer.CustomerStreetAddress;
                addCustomerCommand.Parameters.Add(addressParameter);
                var cityParameter = new SqlParameter("city", SqlDbType.VarChar);
                cityParameter.Value = customer.CustomerCity;
                addCustomerCommand.Parameters.Add(cityParameter);
                var stateParameter = new SqlParameter("state", SqlDbType.VarChar);
                stateParameter.Value = customer.CustomerState;
                addCustomerCommand.Parameters.Add(stateParameter);
                var zipParameter = new SqlParameter("zip", SqlDbType.VarChar);
                zipParameter.Value = customer.CustomerZip;
                addCustomerCommand.Parameters.Add(zipParameter);
                var phoneParameter = new SqlParameter("phone", SqlDbType.VarChar);
                phoneParameter.Value = customer.CustomerPhone;
                addCustomerCommand.Parameters.Add(phoneParameter);

                addCustomerCommand.ExecuteNonQuery();
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

        public List<Customer> GetAllCustomers()
        {
            _sqlConnection.Open();

            try
            {
                var getAllCustomersCommand = _sqlConnection.CreateCommand();
                getAllCustomersCommand.CommandText = @"
                    SELECT CustomerId, CustomerName, CustomerStreetAddress, CustomerCity, CustomerState, CustomerZip, CustomerPhone 
                    FROM Customer";

                var reader = getAllCustomersCommand.ExecuteReader();

                var customers = new List<Customer>();
                while (reader.Read())
                {
                    var customer = new Customer
                    {
                        CustomerId = reader.GetInt32(0),
                        CustomerName = reader.GetString(1),
                        CustomerStreetAddress = reader.GetString(2),
                        CustomerCity = reader.GetString(3),
                        CustomerState = reader.GetString(4),
                        CustomerZip = reader.GetString(5),
                        CustomerPhone = reader.GetString(6)
                    };

                    customers.Add(customer);
                }

                return customers;
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

            return new List<Customer>();
        }

        public Customer GetCustomerById(int customerId)
        {
            _sqlConnection.Open();

            try
            {
                var getAllCustomersCommand = _sqlConnection.CreateCommand();
                getAllCustomersCommand.CommandText = @"
                    SELECT CustomerId, CustomerName, CustomerStreetAddress, CustomerCity, CustomerState, CustomerZip, CustomerPhone 
                    FROM Customer
                    WHERE CustomerId = @customerId";
                var customerIdParameter = new SqlParameter("customerId", SqlDbType.Int);
                customerIdParameter.Value = customerId;
                getAllCustomersCommand.Parameters.Add(customerIdParameter);

                var reader = getAllCustomersCommand.ExecuteReader();

                var customers = new List<Customer>();
                if (reader.Read())
                {
                    var customer = new Customer
                    {
                        CustomerId = reader.GetInt32(0),
                        CustomerName = reader.GetString(1),
                        CustomerStreetAddress = reader.GetString(2),
                        CustomerCity = reader.GetString(3),
                        CustomerState = reader.GetString(4),
                        CustomerZip = reader.GetString(5),
                        CustomerPhone = reader.GetString(6)
                    };
                    return customer;
                }
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

            return null;
        }
    }
}

