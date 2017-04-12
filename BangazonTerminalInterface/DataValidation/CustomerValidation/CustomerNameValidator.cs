using BangazonTerminalInterface.Interfaces.CustomerValidationInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.CustomerValidation
{
    class CustomerNameValidator : ICustomerNameValidator
    {

        public bool ValidateName(string name)
        {
            bool validName = Regex.IsMatch(name, @"[\w]+\s[\w]+");
            bool isNumeric = Regex.IsMatch(name, @"[0-9]");

            if (validName && !isNumeric)
                return true;
            else
                return false;
        }
    }
}