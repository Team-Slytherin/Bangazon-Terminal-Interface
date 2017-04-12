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
using BangazonTerminalInterface.Interfaces.CustomerValidationInterfaces;

namespace BangazonTerminalInterface.Controllers
{
    public class CustomerController
    {
        Customer customer = new Customer();

        private bool UserContinue = true;
        private bool IsComplete = false;
        private bool firstAttempt = true;
        private ICustomerNameValidator _customerName;
        private Interfaces.IConsoleHelper _consoleHelper;
        private ICustomerAddressValidator _customerAddress;
        private ICustomerCityValidation _customerCity;

        public CustomerController()
        {
            _customerName = new CustomerNameValidator();
            _consoleHelper = new ConsoleHelper();
            _customerAddress = new CustomerAddressValidator();
            _customerCity = new CustomerCityValidator();
        }

        public CustomerController (ICustomerNameValidator nameValidator, IConsoleHelper consoleHelper, ICustomerAddressValidator addressValidator, ICustomerCityValidation cityValidator)
        {
            _customerName = nameValidator;
            _consoleHelper = consoleHelper;
            _customerAddress = addressValidator;
            _customerCity = cityValidator;
        }


        public void CreateCustomer ()
        {
            // Get/Validate New Customer's Name
            while(true)
            {
                var input = EnterName(firstAttempt);
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerName.ValidateName(input))
                {
                    customer.CustomerName = input;
                    firstAttempt = true;
                    break;
                }
                else
                {
                    firstAttempt = false;
                    _consoleHelper.WriteLine("Invalid. Please enter First and Last Name");
                }
            }
            // Get/Validate New Customer's Address
            while (true)
            {
                var input = EnterStreetAddress(firstAttempt);
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerAddress.ValidateStreetAddress(input))
                {
                    customer.CustomerStreetAddress = input;
                    firstAttempt = true;
                    break;
                }
                else
                {
                    firstAttempt = false;
                    _consoleHelper.WriteLine("Invalid. Please enter address as 123 main st.");
                }
            }
            // Get/Validate New Customer's City
            while (true)
            {
                var input = EnterCity(firstAttempt);
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerCity.ValidateCity(input))
                {
                    customer.CustomerStreetAddress = input;
                    break;
                }
                else
                {
                    firstAttempt = false;
                    _consoleHelper.WriteLine("Invalid. City must contain 2 characters.");
                }
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

        public string EnterName(bool attempt)
        {
            if (attempt)
            {
                _consoleHelper.WriteHeaderToConsole("Customer Name");
            }

            return _consoleHelper.WriteAndReadFromConsole("Enter Name > ");
        }

        private string EnterStreetAddress(bool attempt)
        {
            if (attempt)
            {
                _consoleHelper.WriteHeaderToConsole("Customer Street Address");
            }

            return _consoleHelper.WriteAndReadFromConsole("Enter Address > ");
        }

        private string EnterCity(bool attempt)
        {
            if (attempt)
            {
                _consoleHelper.WriteHeaderToConsole("Customer City");
            }

            return _consoleHelper.WriteAndReadFromConsole("Enter City > ");
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
