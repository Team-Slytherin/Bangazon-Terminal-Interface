using BangazonTerminalInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Interfaces
{
    public interface IPayment
    {
        void RequestPayment(Customer customer);

        void addPaymentToDb();
    }
}
