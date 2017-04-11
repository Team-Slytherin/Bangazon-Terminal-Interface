using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Models;
using BangazonTerminalInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Controllers
{
    public class SelectCustomerController
    {

        CustomerRepository repo = new CustomerRepository();

        public Customer SelectActiveCustomer()
        {
            int ctr = 0;
            Helper.WriteHeaderToConsole("Select a Bangazon Customer");
            var allCustomers = repo.GetAllCustomers();
            foreach (Customer customer in allCustomers)
            {
                ctr++;
                Console.WriteLine($"{ctr}. {customer.CustomerName} - {customer.CustomerStreetAddress} {customer.CustomerCity} {customer.CustomerState}, {customer.CustomerZip}");
            }
            var selection = Helper.WriteToConsole("> ");

            if (!(selection.Equals("")) && Convert.ToInt32(selection) <= allCustomers.Count())
            {
                return allCustomers[Convert.ToInt32(selection) - 1];
            }
            else
            {
                InvalidCustomer();
            }
            return null;
        }
            

        private void InvalidCustomer()
        {
            Console.WriteLine("Invalid Customer selected");
            Thread.Sleep(2000);
            Console.Clear();
            SelectActiveCustomer();
        }
    }
}
