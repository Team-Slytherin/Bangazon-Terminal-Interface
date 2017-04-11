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
                if (activeCustomer != null)
                {
                    string custString = $"Customer: {activeCustomer.CustomerName}";
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
                        cartString = $"Cart({cartDetail.GetTotleItemsInCart(activeCart.CartId)}) {cartDetail.GetCartPrice(activeCart.CartId)}";
                    }
                    string space = new string(' ', (56 - cartString.Length - custString.Length));
                    if(cartString.Length < 1)
                    {
                        Console.WriteLine($"{custString}\n");
                    }else
                    {
                        Console.WriteLine($"{custString}{space}{cartString}\n");
                    }
                }
                Console.WriteLine(
                  "1.Create a customer account" + "\n"
                + "2.Choose active customer" + "\n"
                + "3.Create a payment option" + "\n"
                + "4.Add product to shopping cart" + "\n"
                + "5.Complete an order" + "\n"
                + "6.See product popularity" + "\n"
                + "7.Logout Current User" + "\n"
                + "8.Leave Bangazon!");
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
                            var selectCustomerCtrl2 = new SelectCustomerController();
                            activeCustomer = selectCustomerCtrl2.SelectActiveCustomer();
                        }
                        SHOWPRODUCTS:
                        Console.Clear();
                        Helper.WriteHeaderToConsole("Add Products to Cart");
                        ProductRepository repo = new ProductRepository();

                        var products = repo.GetAllProducts();                      
                        foreach (Product product in products)
                        {
                            Console.WriteLine(product.ProductId + ". " + product.ProductName + "\n");
                        }
                        Console.WriteLine($"{products.Count + 1}" + ". Done adding products.\n");
                        

                        var selectedProduct = Convert.ToInt32(Console.ReadLine());
                        if (selectedProduct >= 1 && selectedProduct <= products.Count)
                        {
                            (new CartController()).addProduct(activeCustomer, selectedProduct);
                            goto SHOWPRODUCTS;
                        }
                        else if (selectedProduct > products.Count + 1)
                        {
                            Console.WriteLine("Please choose a valid product number!");
                            goto SHOWPRODUCTS;
                        }
                        break;
                    case "5":
                        if (activeCustomer == null)
                        {
                            Console.Clear();
                            var selectCustomerCtrl2 = new SelectCustomerController();
                            activeCustomer = selectCustomerCtrl2.SelectActiveCustomer();
                        }
                        var CartAction = new CartController();
                        CartAction.checkout(activeCustomer);
                        break;
                    case "6":
                        ProductRepository popularityRepo = new ProductRepository();
                        popularityRepo.GetProductPopularity();
                        break;
                    case "7":
                        if (activeCustomer != null)
                        {
                            Console.WriteLine($"\nUser {activeCustomer.CustomerName} is being logged out!");
                            activeCustomer = null;
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine($"\nNo customer currently logged in!");
                            Thread.Sleep(1500);
                        }

                        break;
                    case "8":
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