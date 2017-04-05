using BangazonTerminalInterface.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BangazonTerminalInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(57, 35);
            string menuChoice = "0";
            while (menuChoice != "7" && menuChoice != "8")
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
                + "8.SQL connection test" + "\n"
                + "> ");
                Console.ForegroundColor = ConsoleColor.White;
                var userInput = Console.ReadKey(true).KeyChar.ToString();
                switch (userInput)
                {
                    case "1":
                        var CustomerInfo = new CustomerRepository();
                        CustomerInfo.ValidateInput();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        break;
                    case "5":
                        break;
                    case "6":
                        ProductRepository repo = new ProductRepository();
                        var products = repo.GetAllProducts();
                        foreach(Product product in products)
                        {
                            Console.WriteLine(product.ProductName);
                        }
                        
                        break;
                    case "7":
                        Console.WriteLine("Goodbye!");
                        Thread.Sleep(1500);
                        Environment.Exit(0);
                        break;
                    case "8":
                        SQLConnectionTest testConnection = new SQLConnectionTest();
                        testConnection.GetProducts();
                        break;
                    default:
                        Console.WriteLine("please select a valid menu item...");
                        Thread.Sleep(1000);
                        break;
                }
            }
        }
    }
}