using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.PaymentValidation
{
    class AccountNumberValid
    {
        public bool ValidatePaymentAccountNumber(string accountNumberStr)
        {
            bool isValidVisa = Regex.IsMatch(accountNumberStr, @"^4[0-9]{12}(?:[0-9]{3})?$");
            bool isValidMC = Regex.IsMatch(accountNumberStr, @"^[0-9]{16}$");

            if (isValidVisa || isValidMC)
                return true;
            else
                return false;
        }
    }
}
