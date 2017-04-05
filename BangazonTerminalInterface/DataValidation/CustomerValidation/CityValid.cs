using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.CustomerValidation
{
    class CityValid
    {
        public bool ValidateCity(string city)
        {
            // What else can I do to validate?
            bool isNumeric = Regex.IsMatch(city, @"[0-9]");
            if (city.Length > 2 && !isNumeric)
                return true;
            else
            {
                Console.WriteLine("City must have more than 2 Characters and not contain numbers" + "\n"
                    + "> ");
                return false;
            }
        }
    }
}
