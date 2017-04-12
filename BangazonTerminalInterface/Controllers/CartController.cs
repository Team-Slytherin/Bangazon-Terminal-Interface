using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Helpers;
using BangazonTerminalInterface.Models;
using System;
using System.Diagnostics;
using System.Threading;

namespace BangazonTerminalInterface.Controllers
{
    class CartController
    {
        ConsoleHelper _consoleHelper;

        public CartController()
        {
            _consoleHelper = new ConsoleHelper();
        }
        public void addProduct(Customer activeCustomer)
        {
            SHOWPRODUCTS:
            Console.Clear();
            _consoleHelper.WriteHeaderToConsole("Add Products to Cart");
            ProductRepository repo = new ProductRepository();

            var products = repo.GetAllProducts();
            _consoleHelper.WriteLine("Opt  Product              Price       ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.White;
            char spacePad = ' ';
            foreach (Product product in products)
            {
                _consoleHelper.WriteLine(product.ProductId + ". " + product.ProductName.PadRight(24, spacePad).Substring(0, 23) + "$" + product.ProductPrice);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("**************************************");
            Console.ForegroundColor = ConsoleColor.White;
            _consoleHelper.WriteLine($"{products.Count + 1}" + ". Save order and back to main menu");
            _consoleHelper.WriteLine($"{products.Count + 2}" + ". Checkout\n");
            try
            {
                var selectedProduct = Convert.ToInt32(_consoleHelper.WriteAndReadFromConsole("> "));

                if (selectedProduct >= 1 && selectedProduct <= products.Count)
                {
                    var cartRepo = new CartRepository();
                    var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
                    if (activeCart == null)
                    {
                        cartRepo.AddCart(activeCustomer.CustomerId);
                        activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
                    }
                    var cartDetail = new CartDetailRepository();
                    cartDetail.AddProduct(activeCart.CartId, selectedProduct, 1);
                    _consoleHelper.WriteLine("One item has been put into your cart.");
                    Thread.Sleep(1500);
                    goto SHOWPRODUCTS;
                }
                else if (selectedProduct == products.Count + 1)
                {
                    return;
                }
                else if (selectedProduct == products.Count + 2)
                {
                    checkout(activeCustomer);
                }
                else
                {
                    _consoleHelper.WriteLine("Please choose a valid product number!");
                    goto SHOWPRODUCTS;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                _consoleHelper.WriteLine("Please enter the numbers showed on screen!");
                Thread.Sleep(1000);
                goto SHOWPRODUCTS;
            }
        }

        public void checkout(Customer activeCustomer)
        {
            CHOOSEPAYMENT:
            Console.Clear();
            _consoleHelper.WriteHeaderToConsole("Check Out");
            var cartRepo = new CartRepository();
            var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            if (activeCart != null)
            {
                var cartDetail = new CartDetailRepository();

                _consoleHelper.Write($"Your order total is {cartDetail.GetCartPrice(activeCart.CartId)}. Ready to purchase?\n");
                _consoleHelper.Write("(Y/N) > ");
                var userInput = _consoleHelper.ReadLine();

                if (userInput.ToLower() == "y")
                {
                    // get active customer payment option and show
                    var paymentRepo = new PaymentRepository();
                    var payments = paymentRepo.GetAllPayments(activeCustomer.CustomerId);

                    _consoleHelper.WriteLine("\nYour payment options:\n");
                    int counter = 1;
                    foreach (Payment payment in payments)
                    {
                        _consoleHelper.WriteLine(counter + ". " + payment.PaymentType + "\n");
                        counter++;
                    }

                    _consoleHelper.WriteLine(counter + ". " + "Go Back To Main Menu\n");

                    // read userinput
                    var paymentChoice = Convert.ToInt32(_consoleHelper.WriteAndReadFromConsole("Choose payment option >\n"));
                    if (paymentChoice == counter) return;
                    else if (paymentChoice > 0 && paymentChoice < counter)
                    {
                        var paymentId = payments[paymentChoice - 1].PaymentId;
                        // update order active to false
                        cartRepo.EditCartStatus(activeCart.CartId, paymentId);
                        _consoleHelper.WriteLine("Your order is complete! Press any key to return to main menu.\n");
                        _consoleHelper.ReadKey();
                        return;
                    }
                    else
                    {
                        _consoleHelper.WriteLine("Please enter the numbers showed on screen!");
                        goto CHOOSEPAYMENT;
                    }
                }
                return;
            }
            _consoleHelper.WriteLine("Please add some products to your order first. Press any key to return to main menu.\n");
            _consoleHelper.ReadKey();
        }
    }
}
