using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Interfaces
{
    public interface IConsoleHelper
    {
        string WriteAndReadFromConsole(string input);
        void WriteExitCommand();
        void WriteHeaderToConsole(string headerText);
        bool CheckForUserExit(string exitString);
        void WriteLine(string v);
        void Write(string input);
        string ReadLine();
        string ReadKey();
    }
}
