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

        public static void WriteExitCommand()
        {
            Console.Write("Type");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" \"exit\" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("if you would like to retun to the main menu. \n");
        }

        public static void WriteHeaderToConsole (string headerText)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("*********************************************************");
            int headerTextLength = headerText.Length;
            if (headerTextLength % 2 != 0) headerTextLength = headerTextLength - 1;
            string space = new string(' ', (56 - headerText.Length) / 2);
            Console.WriteLine((space + headerText + space));
            Console.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static bool CheckForUserExit (string exitString)
        {
            if (exitString.ToLower().Equals("exit") || exitString.ToLower().Equals("x"))
            {
                return true;
            }
            return false;
        }
    }
}
