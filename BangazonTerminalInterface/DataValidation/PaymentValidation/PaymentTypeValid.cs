using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.DataValidation.PaymentValidation
{
    class PaymentTypeValid
    {
        public bool ValidatePaymentType(string type)
        {
            var lowerType = type.ToLower();
            if (lowerType == "visa" || lowerType == "mastercard")
            {
                return true;
            }
            else {
                return false;
            }
        }
    }
}
