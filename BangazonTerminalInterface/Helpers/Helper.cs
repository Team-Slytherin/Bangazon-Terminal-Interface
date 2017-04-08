using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Helpers
{
    public class Helper
    {
        public static string WriteToConsole(string input)
        {
            Console.Write(input);
            return Console.ReadLine();
        }
    }
}
