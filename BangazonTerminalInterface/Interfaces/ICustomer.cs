using BangazonTerminalInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Interfaces
{
    public interface ICustomer
    {
        // Create

        void AddCustomer(Customer customer);

        // Read
        Customer GetCustomerById(int customerId);
        List<Customer> GetAllCustomers();

    }
}
