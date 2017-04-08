﻿using BangazonTerminalInterface.DAL.Repository;
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
    class CustomerController
    {
        Customer customer = new Customer();
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
            WriteToDb();
        }

        private void EnterName()
        {
            Header("Customer Name");

            CustomerNameValid repo = new CustomerNameValid();

            string customerName = Helper.WriteToConsole("1.Enter Customer Name > ");

            while (!repo.ValidateName(customerName))
            {
                customerName = Helper.WriteToConsole("Invalid input please enter in the format John Smith > ");
            }
            customer.CustomerName = customerName;
        }

        private void EnterStreetAddress()
        {
            Header("Customer Address");

            StreetAddressValid repo = new StreetAddressValid();

            string customerStreetAddress = Helper.WriteToConsole("1.Enter Street Address > ");

            while (!repo.ValidateStreetAddress(customerStreetAddress))
            {
                customerStreetAddress = Helper.WriteToConsole("Invalid Ensure address is in this format 123 Main ST > ");
            }
            customer.CustomerStreetAddress = customerStreetAddress;
        }

        private void EnterCity()
        {
            Header("Customer City");

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
            Header("Customer Phone Number");

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
            Header("Customer Zip");

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
            Header("Customer State");

            StateValid repo = new StateValid();

            string customerState = Helper.WriteToConsole("1.Enter State > ");

            while (!repo.ValidateState(customerState))
            {
                customerState = Helper.WriteToConsole("Invalid please enter the state abbreviation > ");
            }
            customer.CustomerState = customerState;
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
      
        private void WriteToDb()
        {
            CustomerRepository repo = new CustomerRepository();
            repo.AddCustomer(customer);
        }

    }
}
