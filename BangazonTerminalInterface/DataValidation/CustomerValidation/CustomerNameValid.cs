using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.CustomerValidation
{
    class CustomerNameValid : ICustomerNameValid
    {

        public bool ValidateName(string name)
        {
            // This will also capture numbers, but I will check for that using C#
            bool validName = Regex.IsMatch(name, @"[\w]+\s[\w]+");
            bool isNumeric = Regex.IsMatch(name, @"[0-9]");

            // still need to check database for name
            if (validName && !isNumeric)
                return true;
            else
                return false;
        }
    }
}