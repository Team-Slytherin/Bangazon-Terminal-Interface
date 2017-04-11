using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Models;
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
            SetupHeader();
            var allCustomers = repo.GetAllCustomers();
            foreach (Customer customer in allCustomers)
            {
                ctr++;
                Console.WriteLine($"{ctr}. {customer.CustomerName} - {customer.CustomerStreetAddress} {customer.CustomerCity} {customer.CustomerState}, {customer.CustomerZip}");
            }
            var selection = Console.ReadLine();
            if (selection.Contains(ConsoleKey.Escape.ToString()))
            {
                return null;
            }
            if (!(selection.Equals("")) && Convert.ToInt32(selection) <= allCustomers.Count())
            {
                return allCustomers[Convert.ToInt32(selection) - 1];
            }
            return InvalidCustomer();
        }

        private void SetupHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(
                 "*********************************************************" + "\n"
               + "**************   Select a Bangazon Customer *s************" + "\n"
               + "*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;

        }

        private Customer InvalidCustomer()
        {
            Console.Clear();
            SetupHeader();
            Console.WriteLine("Invalid Customer selected");
            Thread.Sleep(2000);
            return SelectActiveCustomer();
        }
    }
}
