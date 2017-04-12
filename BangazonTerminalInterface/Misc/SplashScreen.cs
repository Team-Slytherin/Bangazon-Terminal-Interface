using BangazonTerminalInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BangazonTerminalInterface.Misc
{
    class SplashScreen
    {
        ConsoleHelper _consoleHelper;

        public SplashScreen ()
        {
            _consoleHelper = new ConsoleHelper();
        }
        public void GenerateSplashScreen ()
        {
            Console.SetWindowSize(57, 15);
            Console.ForegroundColor = ConsoleColor.Red;
            _consoleHelper.WriteLine(Environment.NewLine + "   ███   ██      ▄     ▄▀  ██   ▄▄▄▄▄▄   ████▄    ▄    ");
            _consoleHelper.WriteLine("   █  █  █ █      █  ▄▀    █ █ ▀   ▄▄▀   █   █     █   ");
            _consoleHelper.WriteLine("   █▀▀ ▄ █▄▄█ ██   █ █ ▀▄  █▄▄█ ▄▀▀   ▄▀ █   █ ██   █  ");
            Console.ForegroundColor = ConsoleColor.White;
            _consoleHelper.WriteLine("   █  ▄▀ █  █ █ █  █ █   █ █  █ ▀▀▀▀▀▀   ▀████ █ █  █  ");
            Console.ForegroundColor = ConsoleColor.Blue;
            _consoleHelper.WriteLine("   ███      █ █  █ █  ███     █                █  █ █  ");
            _consoleHelper.WriteLine("           █  █   ██         █                 █   ██  ");
            _consoleHelper.WriteLine("          ▀                 ▀                          ");
            Console.ForegroundColor = ConsoleColor.White;
            _consoleHelper.WriteLine(Environment.NewLine + "              A TeamSlytherin Application       ");
            _consoleHelper.Write(Environment.NewLine + "                Press any key to enter");
            Console.ReadKey();
        }
    }
}
