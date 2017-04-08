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
            EnterName();
            Console.Clear();
            EnterStreetAddress();
            Console.Clear();
            EnterCity();
            Console.Clear();
            EnterState();
            Console.Clear();
            EnterZip();
            Console.Clear();
            EnterPhoneNumber();
        }

        private void EnterName()
        {
            Header("Customer Name");

            CustomerNameValid repo = new CustomerNameValid();

            string customerName = WriteToConsole("1.Enter Customer Name > ");

            while (!repo.ValidateName(customerName))
            {
                customerName = WriteToConsole("Invalid input please enter in the format John Smith > ");
            }  
        }

        private void EnterStreetAddress()
        {
            Header("Customer Address");

            StreetAddressValid repo = new StreetAddressValid();

            string customerStreetAddress = WriteToConsole("1.Enter Street Address > ");

            while (!repo.ValidateStreetAddress(customerStreetAddress))
            {
                customerStreetAddress = WriteToConsole("Invalid Ensure address is in this format 123 Main ST > ");
            }
        }

        private void EnterCity()
        {
            Header("Customer City");

            CityValid repo = new CityValid();

            string customerCity = WriteToConsole("1.Enter City > ");

            while (!repo.ValidateCity(customerCity))
            {
                customerCity = WriteToConsole("Invalid City must have 3 characters > ");
            }
        }

        private void EnterPhoneNumber()
        {
            Header("Customer Phone Number");

            PhoneValid repo = new PhoneValid();

            string customerPhone = WriteToConsole("1.Enter Phone Number > ");

            while (!repo.ValidatePhone(customerPhone))
            {
                customerPhone = WriteToConsole("Invalid Phone Number must be in the following format 555-555-5555 > ");
            }
        }

        private void EnterZip()
        {
            Header("Customer Zip");

            ZipValid repo = new ZipValid();

            string customerZip = WriteToConsole("1.Enter Zip > ");

            while (!repo.ValidateZip(customerZip))
            {
                customerZip = WriteToConsole("Invalid input Zip must be 5 numbers > ");
            }
        }

        private void EnterState()
        {
            Header("Customer State");

            StateValid repo = new StateValid();

            string customerState = WriteToConsole("1.Enter State > ");

            while (!repo.ValidateState(customerState))
            {
                customerState = WriteToConsole("Invalid please enter the state abbreviation > ");
            }
        }

        private void Header(string currentTask)
        {
            char padChar = ' ';
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*********************************************************");
            Console.WriteLine(("**                   " + currentTask).PadRight(55, padChar) + "**");
            Console.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private string WriteToConsole(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }

    }
}
