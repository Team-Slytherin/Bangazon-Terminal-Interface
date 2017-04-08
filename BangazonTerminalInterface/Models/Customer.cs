using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Models
{
   public class Customer
    {
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string CustomerStreetAddress { get; set; }

        public string CustomerCity { get; set; }

        public string CustomerState { get; set; }

        public string CustomerZip { get; set; }

        public string CustomerPhone { get; set; }

    }
}
