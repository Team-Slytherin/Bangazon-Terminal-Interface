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
        private bool IsComplete = false;

        public void CreateCustomer ()
        {
            while (!IsComplete)
            {
                EnterName();
                if (!UserContinue) break;
                EnterStreetAddress();
                if (!UserContinue) break;
                EnterCity();
                if (!UserContinue) break;
                EnterPhoneNumber();
                if (!UserContinue) break;
                EnterZip();
                if (!UserContinue) break;
                EnterState();
                if (!UserContinue) break;
                WriteToDb();
                if (!UserContinue) break;
            }
        }

        private void EnterName()
        {
            Helper.WriteHeaderToConsole("Customer Name");

            CustomerNameValid repo = new CustomerNameValid();
            Helper.WriteExitCommand();

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
                Console.WriteLine("Invalid input please enter in the format John Smith.");
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

        private bool EnterCity()
        {
            Helper.WriteHeaderToConsole("Customer City");

            CityValid repo = new CityValid();

            EnterCity:
            string input = Helper.WriteToConsole("Enter City > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidateCity(input))
            {
                Console.WriteLine("Invalid input City must have 3 Characters. ");
                goto EnterCity;
            }
            customer.CustomerCity = input;
            return true;
        }

        private bool EnterPhoneNumber()
        {
            Helper.WriteHeaderToConsole("Customer Phone Number");

            PhoneValid repo = new PhoneValid();

            EnterPhoneNumber:
            string input = Helper.WriteToConsole("Enter Phone Number > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidatePhone(input))
            {
                Console.WriteLine("Invalid Phone Number Must be in the format 555-555-5555. ");
                goto EnterPhoneNumber;
            }
            customer.CustomerPhone = input;
            return true;
        }

        private bool EnterZip()
        {
            Helper.WriteHeaderToConsole("Customer Zip");

            ZipValid repo = new ZipValid();

            EnterZip:
            string input = Helper.WriteToConsole("Enter Zip Code > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidateZip(input))
            {
                Console.WriteLine("Invalid Zip Must be in the format 12345. ");
                goto EnterZip;
            }
            customer.CustomerZip = input;
            return true;
        }

        private bool EnterState()
        {
            Helper.WriteHeaderToConsole("Customer State");

            StateValid repo = new StateValid();

            EnterState:
            string input = Helper.WriteToConsole("Enter State Abbreviation > ");

            bool userContinue = Helper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidateState(input))
            {
                Console.WriteLine("Invalid input must be in the format TN. ");
                goto EnterState;
            }
            customer.CustomerState = input;
            return true;
        }
      
        private void WriteToDb()
        {
            CustomerRepository repo = new CustomerRepository();
            repo.AddCustomer(customer);
            IsComplete = true; // Seems a little hacky, but I have to do this to break out of the while look.
        }

    }
}
