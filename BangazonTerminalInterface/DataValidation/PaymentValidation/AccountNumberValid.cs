using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.PaymentValidation
{
    public class AccountNumberValid
    {
        public bool ValidatePaymentAccountNumber(string accountNumberStr)
        {
            bool isValidCC = Regex.IsMatch(accountNumberStr, @"^\d{4}-\d{4}-\d{4}-\d{4}$");
            if (isValidCC)
                return true;
            else
                return false;
        }
    }
}

