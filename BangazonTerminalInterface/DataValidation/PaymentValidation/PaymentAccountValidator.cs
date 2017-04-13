using BangazonTerminalInterface.Interfaces.PaymentValidationInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.PaymentValidation
{
    public class PaymentAccountValidator : IPaymentAccountValidation
    {
        public bool ValidatePaymentAccountNumber(string accountNumberStr)
        {
            var newAccountNumberStr = accountNumberStr.Replace("-","");
            if (newAccountNumberStr.Length == 16 || newAccountNumberStr.Length == 17)
            {
               return true;
            }
               return false;
        }
    }
}

