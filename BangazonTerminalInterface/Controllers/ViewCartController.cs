using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BangazonTerminalInterface.Models;
using BangazonTerminalInterface.Helpers;
using BangazonTerminalInterface.DAL.Repository;
using System.Threading;

namespace BangazonTerminalInterface.Controllers
{
    class ViewCartController
    {

        CartDetailRepository cartDetail = new CartDetailRepository();

        ConsoleHelper _consoleHelper;

        public ViewCartController()
        {
            _consoleHelper = new ConsoleHelper();
        }

        public void getItemsInCart(Customer activeCustomer)
        {
            START:
            Console.Clear();
            _consoleHelper.WriteHeaderToConsole("Items in Cart");
            char spacePad = ' ';
            _consoleHelper.WriteLine("Product           Qty     Unit Price    Total         ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            var cartItems = cartDetail.GetItemsInCart(activeCustomer.CustomerId);
            foreach (var item in cartItems)
            {
                _consoleHelper.WriteLine(item.ProductName.PadRight(20, spacePad).Substring(0, 19) + spacePad + item.ProductQuantity.ToString().PadRight(9, spacePad).Substring(0, 9) + item.ProductPrice.ToString("C").PadRight(11, spacePad).Substring(0, 11) + item.Total.ToString("C"));
            }
            var cartRepo = new CartRepository();
            var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            if (activeCart == null)
            {
                cartRepo.AddCart(activeCustomer.CustomerId);
                activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            string cartTotalLine = $"Total: ({cartDetail.GetTotalItemsInCart(activeCart.CartId)}) {cartDetail.GetCartPrice(activeCart.CartId)}";
            string space = new string(' ', (56 - cartTotalLine.Length));
            _consoleHelper.WriteLine(space + cartTotalLine);

            string[] menuOptions = new string[] { "Checkout", "Empty Cart", "Return to Main Menu"};
            int counter = 1;
            foreach (var option in menuOptions)
            {
                _consoleHelper.WriteLine($"{counter}. {option} ");
                counter++;
            }
            _consoleHelper.WriteLine("\n");
            var selection = _consoleHelper.WriteAndReadFromConsole("> ");

            if (!(selection.Equals("")))
            {
                if (selection.Equals("1"))
                {
                    (new CartController()).checkout(activeCustomer);
                }
                else if (selection.Equals("2"))
                {
                    //TODO: Implement Empty Cart here
                    _consoleHelper.WriteLine("Empty Cart");
                    Thread.Sleep(2000);
                    return;
                }
                else if (selection.Equals("3"))
                {
                    return;
                }
                else
                {
                    _consoleHelper.WriteLine("Invalid option selected");
                    Thread.Sleep(2000);
                    Console.Clear();
                    goto START;
                }
            }
            else
            {
                _consoleHelper.WriteLine("Invalid option selected");
                Thread.Sleep(2000);
                Console.Clear();
                goto START;
            }


    }

}
}
