using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.CustomerValidation
{
    class CityValid
    {
        public bool ValidateCity(string city)
        {
            // How to validate street address????
            if (city.Length > 2)
                return true;
            else
            {
                Console.WriteLine("City must have more than 2 Characters" + "\n"
                    + "> ");
                return false;
            }
        }
    }
}
