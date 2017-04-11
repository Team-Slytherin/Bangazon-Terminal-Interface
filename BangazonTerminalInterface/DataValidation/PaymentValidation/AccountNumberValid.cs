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
            if (accountNumberStr.Length == 16)
                return true;
            else
                return false;
        }
    }
}

