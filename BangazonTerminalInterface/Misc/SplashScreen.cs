using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Misc
{
    class SplashScreen
    {
        public void GenerateSplashScreen ()
        {
            Console.SetWindowSize(57, 15);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Environment.NewLine + "   ███   ██      ▄     ▄▀  ██   ▄▄▄▄▄▄   ████▄    ▄    ");
            Console.WriteLine("   █  █  █ █      █  ▄▀    █ █ ▀   ▄▄▀   █   █     █   ");
            Console.WriteLine("   █▀▀ ▄ █▄▄█ ██   █ █ ▀▄  █▄▄█ ▄▀▀   ▄▀ █   █ ██   █  ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("   █  ▄▀ █  █ █ █  █ █   █ █  █ ▀▀▀▀▀▀   ▀████ █ █  █  ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   ███      █ █  █ █  ███     █                █  █ █  ");
            Console.WriteLine("           █  █   ██         █                 █   ██  ");
            Console.WriteLine("          ▀                 ▀                          ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Environment.NewLine + "              A TeamSlytherin Application       ");
            Console.Write(Environment.NewLine + "                Press any key to enter");
            Console.ReadKey();
        }
    }
}
