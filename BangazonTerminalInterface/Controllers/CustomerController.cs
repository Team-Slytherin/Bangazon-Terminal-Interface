using BangazonTerminalInterface.DataValidation.CustomerValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Controllers
{
    class CustomerController
    {
        public  CustomerController()
        {
            ValidateName();
            Console.Clear();
            ValidateStreetAddress();
            Console.Clear();
            ValidateCity();
            Console.Clear();
            ValidateState();
            Console.Clear();
            ValidateZip();
            //Console.Clear();
            //ValidatePhoneNumber();
        }

        private void ValidateName()
        {
            CustomerNameValid repo = new CustomerNameValid();
            string customerName = "";
            Console.WriteLine("Enter Customer Name" + "\n"
                    + "> ");

            customerName = Console.ReadLine();
            while (!repo.ValidateName(customerName))
            {
                Console.WriteLine("Invalid input please enter in the format John Smith" + "\n"
                    + "> ");
                customerName = Console.ReadLine();
            }
            
        }

        private void ValidateStreetAddress()
        {
            StreetAddressValid repo = new StreetAddressValid();
            string customerName = "";

            Console.WriteLine("Enter Address" + "\n"
                    + "> ");

            customerName = Console.ReadLine();
            while (!repo.ValidateStreetAddress(customerName))
            {
                Console.WriteLine("Invalid input please enter in format 123 main st" + "\n"
                    + "> ");
                customerName = Console.ReadLine();
            }
        }

        private void ValidateCity()
        {
            CityValid repo = new CityValid();
            string customerCity = "";

            Console.WriteLine("Enter City" + "\n"
                    + "> ");

            customerCity = Console.ReadLine();
            while (!repo.ValidateCity(customerCity))
            {
                Console.WriteLine("City must have more than 2 Characters and not contain numbers" + "\n"
                    + "> ");
                customerCity = Console.ReadLine();
            }
        }

        private void ValidatePhoneNumber()
        {

        }

        private void ValidateZip()
        {
            ZipValid repo = new ZipValid();
            string customerZip = "";

            Console.WriteLine(
              "1.Enter Zip" + "\n"
              + "> ");

            customerZip = Console.ReadLine();
            while (!repo.ValidateZip(customerZip))
            {
                Console.WriteLine("Zip must have 5 numbers and not contain Letters" + "\n"
                 + "> ");
                customerZip = Console.ReadLine();
            }
        }

        private void ValidateState()
        {
            StateValid repo = new StateValid();
            string customerState = "";

            Console.WriteLine(
              "1.Enter State" + "\n"
              + "> ");
            do
            {
                // add the ability to hit a key to escape to main menu
                customerState = Console.ReadLine();
            }
            while (!repo.ValidateState(customerState));
        }
    }
}
