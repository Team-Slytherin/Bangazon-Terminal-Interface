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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                  "*********************************************************" + "\n"
                + "**   Welcome to Bangazon!Command Line Ordering System  **" + "\n"
                + "*********************************************************");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(
                  "1.Create a customer account" + "\n"
                + "2.Choose active customer" + "\n"
                + "3.Create a payment option" + "\n"
                + "4.Add product to shopping cart" + "\n"
                + "5.Complete an order" + "\n"
                + "6.See product popularity" + "\n"
                + "7.Leave Bangazon!" + "\n"
                + "> ");
                Console.ForegroundColor = ConsoleColor.White;
                var userInput = Console.ReadKey(true).KeyChar.ToString();
                switch (userInput)
                {
                    case "1":
                        Console.Clear();
                        var CustomerInfo = new CustomerController();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        if (activeCustomer == null) break;
                    SHOWPRODUCTS:
                        Console.Clear();
                        ProductRepository repo = new ProductRepository();

                        var products = repo.GetAllProducts();
                        foreach (Product product in products)
                        {
                            Console.WriteLine(product.ProductId + ". " + product.ProductName + "\n");
                        }
                        Console.WriteLine("> ");
                        var selectedProduct = Convert.ToInt32(Console.ReadLine());
                        if (selectedProduct >= 1 && selectedProduct < 9)
                        {
                            (new CartController()).addProduct(activeCustomer, selectedProduct);
                            goto SHOWPRODUCTS;
                        }
                        else if (selectedProduct > 9)
                        {
                            Console.WriteLine("Please choose a valid product number!");
                            goto SHOWPRODUCTS;
                        }
                        break;
                    case "5":
                        if (activeCustomer == null) break;
                        var CartAction = new CartController();
                        CartAction.checkout(activeCustomer);
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