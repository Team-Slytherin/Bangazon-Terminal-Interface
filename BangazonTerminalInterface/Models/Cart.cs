using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Components
{
    class Cart
    {
        public int CartId { get; set; }

        public int CustomerId { get; set; }

        public int PaymentId { get; set; }

        public bool Active { get; set; }
    }
}
