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

        ConsoleHelper _consoleHelper;

        public SelectCustomerController ()
        {
            _consoleHelper = new ConsoleHelper();
        }
        public Customer SelectActiveCustomer()
        {
            int ctr = 0;
            _consoleHelper.WriteHeaderToConsole("Select a Bangazon Customer");
            var allCustomers = repo.GetAllCustomers();
            foreach (Customer customer in allCustomers)
            {
                ctr++;
                _consoleHelper.WriteLine($"{ctr}. {customer.CustomerName} - {customer.CustomerStreetAddress} {customer.CustomerCity} {customer.CustomerState}, {customer.CustomerZip}");
            }

            var selection = _consoleHelper.WriteAndReadFromConsole("> ");

            try
            {
                if (!(selection.Equals("")) && Convert.ToInt32(selection) <= allCustomers.Count())
                {
                    return allCustomers[Convert.ToInt32(selection) - 1];
                }
            }
            catch (Exception)
            {
                return InvalidCustomer();
            }

            return InvalidCustomer();
        }
            

        private Customer InvalidCustomer()
        {
            _consoleHelper.WriteLine("Invalid Customer selected");
            Thread.Sleep(2000);
            Console.Clear();
            return SelectActiveCustomer();
        }
    }
}
