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
        private ICustomerNameValidation _customerName;
        private Interfaces.IConsoleHelper _consoleHelper;
        private ICustomerAddressValidation _customerAddress;
        private ICustomerCityValidation _customerCity;
        private ICustomerStateValidation _customerState;
        private ICustomerZipValidation _customerZip;
        private ICustomerPhoneValidation _customerPhone;

        public CustomerController()
        {
            _customerName = new CustomerNameValidator();
            _consoleHelper = new ConsoleHelper();
            _customerAddress = new CustomerAddressValidator();
            _customerCity = new CustomerCityValidator();
            _customerState = new CustomerStateValidator();
            _customerZip = new CustomerZipValidator();
            _customerPhone = new CustomerPhoneValidator();
        }

        public CustomerController (ICustomerNameValidation nameValidator, 
                                    IConsoleHelper consoleHelper, 
                                    ICustomerAddressValidation addressValidator, 
                                    ICustomerCityValidation cityValidator, 
                                    ICustomerStateValidation stateValidator,
                                    ICustomerZipValidation zipValidator,
                                    ICustomerPhoneValidation phoneValidator)
        {
            _customerName = nameValidator;
            _consoleHelper = consoleHelper;
            _customerAddress = addressValidator;
            _customerCity = cityValidator;
            _customerState = stateValidator;
            _customerZip = zipValidator;
            _customerPhone = phoneValidator;
        }


        public void CreateCustomer ()
        {
            // Get/Validate New Customer's Name
            while (true)
            {
                if(firstAttempt == true)
                    _consoleHelper.WriteHeaderToConsole("Customer Name");
                var input = EnterName();
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
                if (firstAttempt == true)
                    _consoleHelper.WriteHeaderToConsole("Customer Address");
                var input = EnterStreetAddress();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerAddress.ValidateStreetAddress(input))
                {
                    customer.CustomerStreetAddress = input;
                    firstAttempt = true;
                    break;
                }
                else
                {
                    _consoleHelper.WriteLine("Invalid. Please enter address as 123 main st.");
                    firstAttempt = false;
                }
                
            }
            // Get/Validate New Customer's City
            while (true)
            {
                if (firstAttempt == true)
                    _consoleHelper.WriteHeaderToConsole("Customer City");
                var input = EnterCity();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerCity.ValidateCity(input))
                {
                    customer.CustomerCity = input;
                    firstAttempt = true;
                    break;
                }
                else
                {
                    _consoleHelper.WriteLine("Invalid. City must contain more than 2 characters.");
                    firstAttempt = false;
                }
            }
            // Get/Validate New Customer's State
            while (true)
            {
                if (firstAttempt == true)
                    _consoleHelper.WriteHeaderToConsole("Customer State");
                var input = EnterState();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerState.ValidateState(input))
                {
                    customer.CustomerState = input;
                    firstAttempt = true;
                    break;
                }
                else
                {
                    firstAttempt = false;
                    _consoleHelper.WriteLine("Invalid. State must be Abbreviated.");
                }
            }
            // Get/Validate New Customer's Zip
            while (true)
            {
                if (firstAttempt == true)
                    _consoleHelper.WriteHeaderToConsole("Customer Zip");
                var input = EnterZip();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerZip.ValidateZip(input))
                {
                    customer.CustomerZip = input;
                    firstAttempt = true;
                    break;
                }
                else
                {
                    firstAttempt = false;
                    _consoleHelper.WriteLine("Invalid. Zip must be 5 numbers.");
                }
            }
            // Get/Validate New Customer's Phone Number
            while (true)
            {
                if (firstAttempt == true)
                    _consoleHelper.WriteHeaderToConsole("Customer Phone Number");
                var input = EnterPhoneNumber();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerPhone.ValidatePhone(input))
                {
                    customer.CustomerPhone = input;
                    IsComplete = true;
                    break;
                }
                else
                {
                    firstAttempt = false;
                    _consoleHelper.WriteLine("Invalid. Phone number must be in the formatt 111-111-1111.");
                }
            }
            // Add To Database
            if (IsComplete)
                WriteToDb(customer);
        }

        public string EnterName()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Name > ");
        }

        private string EnterStreetAddress()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Address > ");
        }

        private string EnterCity()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter City > ");
        }

        private string EnterPhoneNumber()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Phone Number > ");
        }

        private string EnterZip()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Zip > ");
        }

        private string EnterState()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter State Abbreviation > ");
        }
      
        private void WriteToDb(Customer addCustomer)
        {
            CustomerRepository repo = new CustomerRepository();
            repo.AddCustomer(addCustomer);
            //IsComplete = true; // Seems a little hacky, but I have to do this to break out of the while look.
        }

    }
}
