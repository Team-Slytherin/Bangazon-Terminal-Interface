using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.DataValidation.CustomerValidation;
using BangazonTerminalInterface.Helpers;
using BangazonTerminalInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Controllers
{
    public class CustomerController
    {
        Customer customer = new Customer();

        private bool UserContinue = true;
        public CustomerController()
        {
            //EnterName();
            //Console.Clear();
            //EnterStreetAddress();
            //Console.Clear();
            //EnterCity();
            //Console.Clear();
            //EnterState();
            //Console.Clear();
            //EnterZip();
            //Console.Clear();
            //EnterPhoneNumber();
            //WriteToDb();
        }

        public void CreateCustomer ()
        {
            //if (!EnterName())
            //{
            //    return false;
            //}

            //if (!EnterStreetAddress())
            //{
            //    return false;
            //}

            //if (!EnterCity())
            //{
            //    return false;
            //}
            //return true;

            while (UserContinue)
            {
                EnterName();
                if (!UserContinue) break;
                EnterStreetAddress();
            }
        }

        private void EnterName()
        {
            Helper.WriteHeaderToConsole("Customer Name");

            CustomerNameValid repo = new CustomerNameValid();
            EnterName:
            string input = Helper.WriteToConsole("Enter Customer Name > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if(userContinue)
            {
                UserContinue = false;
                return;
            }

            if (!repo.ValidateName(input))
            {
                Console.WriteLine("Invalid input please enter in the format John Smith");
                goto EnterName;
            }

            customer.CustomerName = input;
        }

        private bool EnterStreetAddress()
        {
            Helper.WriteHeaderToConsole("Customer Address");

            StreetAddressValid repo = new StreetAddressValid();

            EnterAddress:
            string input = Helper.WriteToConsole("Enter Customer Address > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }

            if (!repo.ValidateStreetAddress(input))
            {
                Console.WriteLine("Invalid input please enter in the format 123 Main St.");
                goto EnterAddress;
            }

            customer.CustomerStreetAddress = input;
            return true;
        }

        private void EnterCity()
        {
            Helper.WriteHeaderToConsole("Customer City");

            CityValid repo = new CityValid();

            string customerCity = Helper.WriteToConsole("1.Enter City > ");

            while (!repo.ValidateCity(customerCity))
            {
                customerCity = Helper.WriteToConsole("Invalid City must have 3 characters > ");
            }
            customer.CustomerCity = customerCity;
        }

        private void EnterPhoneNumber()
        {
            Helper.WriteHeaderToConsole("Customer Phone Number");

            PhoneValid repo = new PhoneValid();

            string customerPhone = Helper.WriteToConsole("1.Enter Phone Number > ");

            while (!repo.ValidatePhone(customerPhone))
            {
                customerPhone = Helper.WriteToConsole("Invalid Phone Number must be in the following format 555-555-5555 > ");
            }
            customer.CustomerPhone = customerPhone;
        }

        private void EnterZip()
        {
            Helper.WriteHeaderToConsole("Customer Zip");

            ZipValid repo = new ZipValid();

            string customerZip = Helper.WriteToConsole("1.Enter Zip > ");

            while (!repo.ValidateZip(customerZip))
            {
                customerZip = Helper.WriteToConsole("Invalid input Zip must be 5 numbers > ");
            }
            customer.CustomerZip = customerZip;
        }

        private void EnterState()
        {
            Helper.WriteHeaderToConsole("Customer State");

            StateValid repo = new StateValid();

            string customerState = Helper.WriteToConsole("1.Enter State > ");

            while (!repo.ValidateState(customerState))
            {
                customerState = Helper.WriteToConsole("Invalid please enter the state abbreviation > ");
            }
            customer.CustomerState = customerState;
        }
      
        private void WriteToDb()
        {
            CustomerRepository repo = new CustomerRepository();
            repo.AddCustomer(customer);
        }

    }
}
