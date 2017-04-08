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
        public bool ValidatePaymentAccountNumber(int accountNumber)
        {
            bool isValidVisa = Regex.IsMatch(accountNumber.ToString(), @"/^ (?: 4[0 - 9]{ 12} (?:[0 - 9]{ 3})?)$/");
            bool isValidMC = Regex.IsMatch(accountNumber.ToString(), @"/^ (?: 5[1 - 5][0 - 9]{ 14})$");

            if (Convert.ToString(accountNumber).Length == 16 && (isValidVisa || isValidMC))
                return true;
            else
                return false;
        }
    }
}
