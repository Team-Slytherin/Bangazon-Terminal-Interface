using BangazonTerminalInterface.DataValidation.CustomerValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DAL.Repository
{
    class CustomerRepository
    {
        public  CustomerRepository()
        {
            ValidateName();
            ValidateStreetAddress();
            ValidateCity();
            ValidateState();
            ValidateZip();
            ValidatePhoneNumber();
        }

        private void ValidateName()
        {
            CustomerNameValid customerNameValid = new CustomerNameValid();
            string customerName = "";

            Console.WriteLine(
              "1.Enter Customer Name" + "\n"
              + "> ");
            do
            {
                // add the ability to hit a key to excape to main menu
                customerName = Console.ReadLine();
            }
            while (!customerNameValid.ValidateName(customerName));

        }

        private void ValidateStreetAddress()
        {
            throw new NotImplementedException();
        }

        private void ValidateCity()
        {
            throw new NotImplementedException();
        }

        private void ValidatePhoneNumber()
        {
            throw new NotImplementedException();
        }

        private void ValidateZip()
        {
            throw new NotImplementedException();
        }

        private void ValidateState()
        {
            throw new NotImplementedException();
        }

    }
}

// Regex examples
// Regex for 1 + 1 or 1+1
//Regex r1 = new Regex(@"^(\d+)\s*([+-/%*])\s*(\d+)$");
//Match match1 = r1.Match(input);
//// Regex for a = 8 or a=8
//Regex r2 = new Regex(@"^([a-zA-Z])\s*=\s*(\d*)$");
//Match match2 = r2.Match(input);
//// Regex for x only
//Regex r3 = new Regex(@"^([a-zA-Z])$");
//Match match3 = r3.Match(input);
//// Regex for x + 1 or x+1
//Regex r4 = new Regex(@"^([a-zA-Z])\s*([+-/%*])\s*(\d+)$");
//Match match4 = r4.Match(input);
//// Regex for 1 + x or 1+x
//Regex r5 = new Regex(@"^(\d+)\s*([+-/%*])\s*([a-zA-Z])$");
//Match match5 = r5.Match(input);
//// Regex for 1 + 1 or 1+1
//Regex r6 = new Regex(@"^(last)$");
//Match match6 = r6.Match(input);
//// Regex for 1 + 1 or 1+1
//Regex r7 = new Regex(@"^(lastq)$");
//Match match7 = r7.Match(input);
