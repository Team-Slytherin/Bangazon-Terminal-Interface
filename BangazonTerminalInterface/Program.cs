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
            SHOWMENU:
            {
                Console.Clear();
                Helper.WriteHeaderToConsole("Welcome to Bangazon!");
                Console.WriteLine(
                  "1.Create a customer account" + "\n"
                + "2.Choose active customer" + "\n"
                + "3.Create a payment option" + "\n"
                + "4.Add product to shopping cart" + "\n"
                + "5.Complete an order" + "\n"
                + "6.See product popularity" + "\n"
                + "7.Leave Bangazon!");
                Console.ForegroundColor = ConsoleColor.White;
                var userInput = Helper.WriteToConsole("> ");
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
                        var paymentCtrl = new PaymentController(activeCustomer);
                        paymentCtrl.addNewPayment();
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
                        (new CartController()).checkout(activeCustomer);
                        break;
                    case "6":
                        ProductRepository popularityRepo = new ProductRepository();
                        popularityRepo.GetProductPopularity();
                        break;
                    case "7":
                        Console.WriteLine("Goodbye!");
                        Thread.Sleep(1500);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("please select a valid menu item...");
                        Thread.Sleep(1000);
                        break;
                }
                goto SHOWMENU;
            }
        }
    }
}