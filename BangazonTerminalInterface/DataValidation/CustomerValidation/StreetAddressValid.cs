using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.CustomerValidation
{
    class StreetAddressValid
    {
        public bool ValidateStreetAddress(string street)
        {
            bool isNumeric = Regex.IsMatch(street, @"[0-9]");
            if (isNumeric)
                return true;
            else
                return false;
        }
    }
}


// checks for numbers in the string
//bool isNumeric = Regex.IsMatch(state, @"[0-9]");
//            if(state.Length == 2 && !isNumeric)
//            {
//                return true;
//            }
//            else
//            {
//                consoleHelper.WriteLine("Invalid Please enter the abbreviate for the state." + "\n"
//                    + "> ");
//                return false;
//            }