using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.Controllers;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BangazonTerminalInterface.Misc;
using BangazonTerminalInterface.Helpers;

namespace BangazonTerminalInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            SplashScreen splash = new SplashScreen();
            splash.GenerateSplashScreen();
            Console.SetWindowSize(57, 35);
            Customer activeCustomer = null;
            ConsoleHelper consoleHelper = new ConsoleHelper();
            SHOWMENU:
            {
                Console.Clear();
                consoleHelper.WriteHeaderToConsole("Welcome to Bangazon!");
                if (activeCustomer != null)
                {
                    string custString = $"Logged In As: {activeCustomer.CustomerName}";
                    string cartString = "";
                    var cartRepo = new CartRepository();
                    var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
                    if (activeCart == null)
                    {
                        cartRepo.AddCart(activeCustomer.CustomerId);
                        activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
                    }
                    else
                    {
                        var cartDetail = new CartDetailRepository();
                        cartString = $"Cart({cartDetail.GetTotalItemsInCart(activeCart.CartId)}) {cartDetail.GetCartPrice(activeCart.CartId)}";
                    }
                    string space = new string(' ', (56 - cartString.Length - custString.Length));
                    if (cartString.Length < 1)
                    {
                        consoleHelper.WriteLine($"{custString}\n");
                    }
                    else
                    {
                        consoleHelper.WriteLine($"{custString}{space}{cartString}\n");
                    }
                }
                consoleHelper.WriteLine(
                  "1.Create a new customer account" + "\n"
                + "2.Choose an existing customer" + "\n"
                + "3.Create a new payment option" + "\n"
                + "4.Add product(s) to shopping cart" + "\n"
                + "5.View Items in Cart\n"
                + "6.Complete an order" + "\n"
                + "7.View product popularity" + "\n"
                + "8.Logout Current User" + "\n"
                + "9.Leave Bangazon!");
                Console.ForegroundColor = ConsoleColor.White;
                var userInput = consoleHelper.WriteAndReadFromConsole("> ");
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        var CustomerInfo = new CustomerController();
                        CustomerInfo.CreateCustomer();
                        break;
                    case "2":
                        Console.Clear();
                        var selectCustomerCtrl = new SelectCustomerController();
                        activeCustomer = selectCustomerCtrl.SelectActiveCustomer();
                        break;
                    case "3":
                        if (activeCustomer == null)
                        {
                            Console.Clear();
                            var selectCustomerCtrl1 = new SelectCustomerController();
                            activeCustomer = selectCustomerCtrl1.SelectActiveCustomer();
                        }
                        Console.Clear();
                        var PaymentCtrl = new PaymentController();
                        PaymentCtrl.RequestPayment(activeCustomer);
                        break;
                    case "4":
                        if (activeCustomer == null)
                        {
                            Console.Clear();

                            activeCustomer = (new SelectCustomerController()).SelectActiveCustomer();
                        }
                        (new CartController()).addProduct(activeCustomer);

                        break;
                    case "5":
                        if (activeCustomer == null)
                        {
                            Console.Clear();

                            activeCustomer = (new SelectCustomerController()).SelectActiveCustomer();
                        }
                        (new ViewCartController()).getItemsInCart(activeCustomer);

                        break;
                    case "6":
                        if (activeCustomer == null)
                        {
                            Console.Clear();
                            activeCustomer = (new SelectCustomerController()).SelectActiveCustomer();
                        }
                        (new CartController()).checkout(activeCustomer);
                        break;
                    case "7":
                        ProductRepository popularityRepo = new ProductRepository();
                        popularityRepo.GetProductPopularity();
                        break;
                    case "8":
                        if (activeCustomer != null)
                        {
                            consoleHelper.WriteLine($"\nUser {activeCustomer.CustomerName} is being logged out!");
                            activeCustomer = null;
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            consoleHelper.WriteLine($"\nNo customer currently logged in!");
                            Thread.Sleep(1500);
                        }

                        break;
                    case "9":
                        Console.Clear();
                        consoleHelper.WriteHeaderToConsole("Logging out...");
                        Thread.Sleep(1500);
                        Environment.Exit(0);
                        break;                        
                    default:
                        consoleHelper.WriteLine("please select a valid menu item...");
                        Thread.Sleep(1000);
                        break;
                }
                goto SHOWMENU;
            }
        }
    }
}