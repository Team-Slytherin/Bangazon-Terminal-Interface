using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangazonTerminalInterface.Interfaces;

namespace BangazonTerminalInterface.Helpers
{
    public class ConsoleHelper : IConsoleHelper
    {
        public string WriteAndReadFromConsole(string input)
        {
            Write(input);
            return ReadLine();
        }

        public void WriteExitCommand()
        {
            Write("Type");
            Console.ForegroundColor = ConsoleColor.Red;
            Write(" \"exit\" ");
            Console.ForegroundColor = ConsoleColor.White;
            Write("if you would like to return to the main menu. \n");
        }

        public void WriteHeaderToConsole(string headerText)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine("*********************************************************");
            int headerTextLength = headerText.Length;
            if (headerTextLength % 2 != 0) headerTextLength = headerTextLength - 1;
            string space = new string(' ', (56 - headerText.Length) / 2);
            WriteLine((space + headerText + space));
            WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public bool CheckForUserExit(string exitString)
        {
            if (exitString.ToLower().Equals("exit") || exitString.ToLower().Equals("x"))
            {
                return true;
            }
            return false;
        }

        public void WriteLine(string input)
        {
            Console.WriteLine(input);
        }

        public void Write (string input)
        {
            Console.Write(input);
        }

        public string ReadLine ()
        {
            return Console.ReadLine();
        }

        public string ReadKey()
        {
            return Console.ReadKey().Key.ToString();         
        }
    }
}
