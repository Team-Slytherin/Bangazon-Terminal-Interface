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
            _consoleHelper.WriteLine("Product                Qty     Unit Price    Total       ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            var cartItems = cartDetail.GetItemsInCart(activeCustomer.CustomerId);
            foreach (var item in cartItems)
            {
                _consoleHelper.WriteLine(item.ProductName.PadRight(23, spacePad).Substring(0, 23) + item.ProductQuantity.ToString().PadRight(8, spacePad).Substring(0, 8) + '$' + item.ProductPrice.ToString().Remove(item.ProductPrice.ToString().Length - 2).PadLeft(7, spacePad).PadRight(13, spacePad).Substring(0, 13) + '$' + item.Total.ToString().Remove(item.ProductPrice.ToString().Length - 2).PadLeft(7, spacePad));
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

            string[] menuOptions = new string[] { "Checkout", "Empty Cart"};
            int counter = 1;
            foreach (var option in menuOptions)
            {
                _consoleHelper.WriteLine($"{counter}. {option} ");
                counter++;
            }
            _consoleHelper.WriteExitCommand();
            var selection = _consoleHelper.WriteAndReadFromConsole("> ");

            if (!(selection.Equals("")))
            {
                if (_consoleHelper.CheckForUserExit(selection)) { return; };
                if (selection.Equals("1"))
                {
                    (new CartController()).checkout(activeCustomer);
                }
                else if (selection.Equals("2"))
                {
                    cartRepo.EmptyCart(activeCustomer.CustomerId);
                    _consoleHelper.WriteLine("Cart has been emptied!");
                    Thread.Sleep(2000);
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
