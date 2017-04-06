using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.CustomerValidation
{
    class PhoneValid
    {
        public bool ValidatePhone(string phone)
        {
            // checks for numbers in the string
            bool isValidPhoneNumber = Regex.IsMatch(phone, @"^((((\(\d{3}\))|(\d{3}-))\d{3}-\d{4})|(\+?\d{2}((-| )\d{1,8}){1,5}))(( x| ext)\d{1,5}){0,1}$");
            if (phone.Length == 12 && isValidPhoneNumber)
                return true;
            else
                return false;
        }
    }
}


///(?:(?:\+?1\s*(?:[.-]\s*)?)?(?:(\s*([2-9]1[02-9]|[2-9][02-8]‌​1|[2-9][02-8][02-9])‌​\s*)|([2-9]1[02-9]|[‌​2-9][02-8]1|[2-9][02‌​-8][02-9]))\s*(?:[.-‌​]\s*)?)([2-9]1[02-9]‌​|[2-9][02-9]1|[2-9][‌​02-9]{2})\s*(?:[.-]\‌​s*)?([0-9]{4})/