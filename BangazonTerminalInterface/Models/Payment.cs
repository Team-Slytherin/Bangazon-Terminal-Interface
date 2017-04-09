using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Components
{
    class Payment
    {
        public int PaymentId { get; set; }

        public int CustomerId { get; set; }

        public string PaymentType { get; set; }

        public long PaymentAccountNumber { get; set; }

    }
}
