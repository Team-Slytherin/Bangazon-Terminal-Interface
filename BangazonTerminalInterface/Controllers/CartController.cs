using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Helpers;
using BangazonTerminalInterface.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace BangazonTerminalInterface.Controllers
{
    class CartController
    {
        CartDetailRepository cartDetail = new CartDetailRepository();
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
            _consoleHelper.WriteLine("Opt Product               Price                        ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            char spacePad = ' ';
            foreach (Product product in products)
            {
                StringBuilder productIdString = new StringBuilder();
                string productId = productIdString.Append(product.ProductId.ToString()).Append(".").ToString();
                _consoleHelper.WriteLine(productId.PadRight(8, spacePad).Substring(0, 4) + product.ProductName.PadRight(22, spacePad).Substring(0, 22) + "$" + product.ProductPrice.ToString().PadLeft(7, spacePad));
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            var cartRepo = new CartRepository();
            var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            if (activeCart == null)
            {
                cartRepo.AddCart(activeCustomer.CustomerId);
                activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            }
            string cartTotalLine = $"Total: ({cartDetail.GetTotalItemsInCart(activeCart.CartId)}) {cartDetail.GetCartPrice(activeCart.CartId)}";
            string space = new string(' ', (56 - cartTotalLine.Length));
            _consoleHelper.WriteLine(space + cartTotalLine);
            _consoleHelper.WriteLine($"{products.Count + 1}" + ".  Save Cart and Return to Main Menu");
            _consoleHelper.WriteLine($"{products.Count + 2}" + ".  Checkout\n");
            try
            {
                var selectedProduct = Convert.ToInt32(_consoleHelper.WriteAndReadFromConsole("> "));

                if (selectedProduct >= 1 && selectedProduct <= products.Count)
                {
                   
                    var cartDetail = new CartDetailRepository();
                    cartDetail.AddProduct(activeCart.CartId, selectedProduct, 1);
                    _consoleHelper.WriteLine("One item has been put into your cart.");
                    Thread.Sleep(500);
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
                if (cartDetail.GetTotalItemsInCart(activeCart.CartId) < 1)
                {
                    _consoleHelper.ErrorMessage("Your cart is empty. Please continue shopping.");
                    (new CartController()).addProduct(activeCustomer);
                }
                _consoleHelper.Write($"Your order total is {cartDetail.GetCartPrice(activeCart.CartId)}. Ready to purchase?\n");
                _consoleHelper.WriteExitCommand();
                var userInput = _consoleHelper.WriteAndReadFromConsole("Y/N > ");
                _consoleHelper.CheckForUserExit(userInput);

                if (userInput.ToLower() == "y")
                {
                    Console.Clear();
                    _consoleHelper.WriteHeaderToConsole("Choose Payment");
                    // get active customer payment option and show
                    var paymentRepo = new PaymentRepository();
                    var payments = paymentRepo.GetAllPayments(activeCustomer.CustomerId);

                    if(payments.Count < 1)
                    {
                        var paymentController = new PaymentController();
                        var payment = paymentController.RequestPayment(activeCustomer);
                        if(payment == null) { return; };
                        goto CHOOSEPAYMENT;
                    }
                    _consoleHelper.WriteLine("\nCustomer Payment Options:\n");
                    int counter = 1;
                    foreach (Payment payment in payments)
                    {
                        _consoleHelper.WriteLine(counter + ". " + payment.PaymentType + "  ****-****-****-" + payment.PaymentAccountNumber.ToString().Substring(payment.PaymentAccountNumber.ToString().Length - 4) + "\n");
                        counter++;
                    }

                    _consoleHelper.WriteExitCommand();

                    // read userinput
                    var paymentChoice = _consoleHelper.WriteAndReadFromConsole("Choose payment option > ");
                    if (_consoleHelper.CheckForUserExit(paymentChoice)) { return; };
                    try
                    {
                        var intPaymentChoice = Convert.ToInt32(paymentChoice);
                        if (intPaymentChoice == counter) return;
                        else if (intPaymentChoice > 0 && intPaymentChoice < counter)
                        {
                            var paymentId = payments[intPaymentChoice - 1].PaymentId;
                            // update order active to false
                            cartRepo.EditCartStatus(activeCart.CartId, paymentId);
                            _consoleHelper.WriteLine("\nYour order is complete! Press any key to return to main menu.\n");
                            _consoleHelper.ReadKey();
                            return;
                        }
                        else
                        {
                            _consoleHelper.WriteLine("Please enter a valid option.");
                            goto CHOOSEPAYMENT;
                        }
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        Debug.WriteLine(ex.StackTrace);
                    }
                }
                return;
            }
            _consoleHelper.WriteLine("Please add some products to your order first. Press any key to return to main menu.\n");
            _consoleHelper.ReadKey();
        }
    }
}
