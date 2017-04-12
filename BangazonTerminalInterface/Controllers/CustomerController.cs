using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.DataValidation.CustomerValidation;
using BangazonTerminalInterface.Helpers;
using BangazonTerminalInterface.Models;
using BangazonTerminalInterface.Interfaces;
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

        public CustomerController()
        {
            _customerName = new CustomerNameValid();
            _consoleHelper = new ConsoleHelper();
        }

        public CustomerController (ICustomerNameValid nameValidator, IConsoleHelper consoleHelper)
        {
            _customerName = nameValidator;
            _consoleHelper = consoleHelper;
        }

        private bool UserContinue = true;
        private bool IsComplete = false;
        private ICustomerNameValid _customerName;
        private Interfaces.IConsoleHelper _consoleHelper;

        public void CreateCustomer ()
        {
            while(true)
            {
                var input = EnterName();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if(_customerName.ValidateName(input)) { break; };
            }

            //while (!IsComplete)
            //{

            //EnterName();
            //if (!UserContinue) break;
            //EnterStreetAddress();
            //if (!UserContinue) break;
            //EnterCity();
            //if (!UserContinue) break;
            //EnterPhoneNumber();
            //if (!UserContinue) break;
            //EnterZip();
            //if (!UserContinue) break;
            //EnterState();
            //if (!UserContinue) break;
            //WriteToDb();
            //if (!UserContinue) break;
            //}
        }

        //public bool EnterName()
        //{
        //    _consoleHelper.WriteHeaderToConsole("Customer Name");

            
        //    _consoleHelper.WriteExitCommand();

        //    ENTERNAME:
        //    string input = _consoleHelper.WriteAndReadFromConsole("Enter Customer Name > ");

        //    bool userExit = _consoleHelper.CheckForUserExit(input);

        //    if(userExit)
        //    {
        //        return false;
        //    }

        //    if (!_customerName.ValidateName(input))
        //    {
        //        _consoleHelper.WriteLine("Invalid input please enter in the format John Smith.");
        //        goto ENTERNAME;
        //    }

        //    customer.CustomerName = input;
        //    return true;
        //}

        public string EnterName()
        {
            _consoleHelper.WriteHeaderToConsole("Customer Name");

            return _consoleHelper.WriteAndReadFromConsole("Enter Customer Name > ");
        }

        private bool EnterStreetAddress()
        {
            _consoleHelper.WriteHeaderToConsole("Customer Address");

            StreetAddressValid repo = new StreetAddressValid();

            EnterAddress:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter Customer Address > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }

            if (!repo.ValidateStreetAddress(input))
            {
                _consoleHelper.WriteLine("Invalid input please enter in the format 123 Main St.");
                goto EnterAddress;
            }

            customer.CustomerStreetAddress = input;
            return true;
        }

        private bool EnterCity()
        {
            _consoleHelper.WriteHeaderToConsole("Customer City");

            CityValid repo = new CityValid();

            EnterCity:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter City > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidateCity(input))
            {
                _consoleHelper.WriteLine("Invalid input City must have 3 Characters. ");
                goto EnterCity;
            }
            customer.CustomerCity = input;
            return true;
        }

        private bool EnterPhoneNumber()
        {
            _consoleHelper.WriteHeaderToConsole("Customer Phone Number");

            PhoneValid repo = new PhoneValid();

            EnterPhoneNumber:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter Phone Number > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidatePhone(input))
            {
                _consoleHelper.WriteLine("Invalid Phone Number Must be in the format 555-555-5555. ");
                goto EnterPhoneNumber;
            }
            customer.CustomerPhone = input;
            return true;
        }

        private bool EnterZip()
        {
            _consoleHelper.WriteHeaderToConsole("Customer Zip");

            ZipValid repo = new ZipValid();

            EnterZip:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter Zip Code > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidateZip(input))
            {
                _consoleHelper.WriteLine("Invalid Zip Must be in the format 12345. ");
                goto EnterZip;
            }
            customer.CustomerZip = input;
            return true;
        }

        private bool EnterState()
        {
            _consoleHelper.WriteHeaderToConsole("Customer State");

            StateValid repo = new StateValid();

            EnterState:
            string input = _consoleHelper.WriteAndReadFromConsole("Enter State Abbreviation > ");

            bool userContinue = _consoleHelper.CheckForUserExit(input);

            if (userContinue)
            {
                return false;
            }
            while (!repo.ValidateState(input))
            {
                _consoleHelper.WriteLine("Invalid input must be in the format TN. ");
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
