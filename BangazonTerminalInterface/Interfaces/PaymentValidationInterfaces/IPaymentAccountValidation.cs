using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Interfaces.PaymentValidationInterfaces
{
    public interface IPaymentAccountValidation
    {
        bool ValidatePaymentAccountNumber(string accountNumber);
    }
}
