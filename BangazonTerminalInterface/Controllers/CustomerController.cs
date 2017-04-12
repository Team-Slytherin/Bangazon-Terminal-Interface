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
using System.Threading;

namespace BangazonTerminalInterface.Controllers
{
    public class CustomerController
    {
        Customer customer = new Customer();
        private bool IsComplete = false;
        private ICustomerNameValidation _customerName;
        private IConsoleHelper _consoleHelper;
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


        public Customer CreateCustomer ()
        {
            // Get/Validate New Customer's Name
            while (true)
            {
                CurrentCustomer();
                var input = EnterName();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerName.ValidateName(input))
                {
                    Console.Clear();
                    customer.CustomerName = input;
                    break;
                }
                else
                {
                    _consoleHelper.ErrorMessage("Invalid. Please enter first and last name.");
                }
            }
            // Get/Validate New Customer's Address
            while (true)
            {
                CurrentCustomer();
                var input = EnterStreetAddress();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerAddress.ValidateStreetAddress(input))
                {
                    Console.Clear();
                    customer.CustomerStreetAddress = input;
                    break;
                }
                else
                {
                    _consoleHelper.ErrorMessage("Invalid. Please enter in the following format 123 main st.");
                }
            }
            // Get/Validate New Customer's City
            while (true)
            {
                CurrentCustomer();
                var input = EnterCity();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerCity.ValidateCity(input))
                {
                    Console.Clear();
                    customer.CustomerCity = input;
                    break;
                }
                else
                {
                    _consoleHelper.ErrorMessage("Invalid. City must contain at least 3 characters.");
                }
            }
            // Get/Validate New Customer's State
            while (true)
            {
                CurrentCustomer();
                var input = EnterState();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerState.ValidateState(input))
                {
                    customer.CustomerState = input;
                    Console.Clear();
                    break;
                }
                else
                {
                    _consoleHelper.ErrorMessage("Invalid. State must be Abbreviated.");
                }
            }
            // Get/Validate New Customer's Zip
            while (true)
            {
                CurrentCustomer();
                var input = EnterZip();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerZip.ValidateZip(input))
                {
                    Console.Clear();
                    customer.CustomerZip = input;
                    break;
                }
                else
                {
                    _consoleHelper.ErrorMessage("Invalid. Zip must be 5 numbers.");
                }
            }
            // Get/Validate New Customer's Phone Number
            while (true)
            {
                CurrentCustomer();
                var input = EnterPhoneNumber();
                if (_consoleHelper.CheckForUserExit(input)) { break; };
                if (_customerPhone.ValidatePhone(input))
                {
                    Console.Clear();
                    customer.CustomerPhone = input;
                    IsComplete = true;
                    break;
                }
                else
                {
                    _consoleHelper.ErrorMessage("Invalid. Phone number must be in the format 111-111-1111.");
                }
            }
            // Add To Database
            if (IsComplete)
                WriteToDb(customer);
            return customer;
        }

        public string EnterName()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Name > ");
        }

        public string EnterStreetAddress()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Address > ");
        }

        public string EnterCity()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter City > ");
        }

        public string EnterPhoneNumber()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Phone Number > ");
        }

        public string EnterZip()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter Zip > ");
        }

        public string EnterState()
        {
            return _consoleHelper.WriteAndReadFromConsole("Enter State Abbreviation > ");
        }

        public void WriteToDb(Customer addCustomer)
        {
            CustomerRepository repo = new CustomerRepository();
            repo.AddCustomer(addCustomer);
            //IsComplete = true; // Seems a little hacky, but I have to do this to break out of the while look.
        }

        public void CurrentCustomer()
        {
            _consoleHelper.WriteHeaderToConsole("Create Customer");
            _consoleHelper.WriteLine($"Name: {customer.CustomerName}");
            _consoleHelper.WriteLine($"Address: {customer.CustomerStreetAddress}");
            _consoleHelper.WriteLine($"City: {customer.CustomerCity}");
            _consoleHelper.WriteLine($"State: {customer.CustomerState}");
            _consoleHelper.WriteLine($"Zip: {customer.CustomerZip}");
            _consoleHelper.WriteLine($"Phone: {customer.CustomerPhone}");
        }

    }
}
