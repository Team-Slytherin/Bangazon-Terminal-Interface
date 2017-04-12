using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Models;
using System;
using BangazonTerminalInterface.Helpers;

namespace BangazonTerminalInterface.Controllers
{
    class CartController
    {
        ConsoleHelper _consoleHelper;
        public CartController ()
        {
            _consoleHelper = new ConsoleHelper();
        }
        public void addProduct(Customer activeCustomer, int selectedProductId)
        {
            var cartRepo = new CartRepository();
            var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            if (activeCart == null)
            {
                cartRepo.AddCart(activeCustomer.CustomerId);
                activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            }
            var cartDetail = new CartDetailRepository();
            cartDetail.AddProduct(activeCart.CartId, selectedProductId, 1);
        }

        public void checkout(Customer activeCustomer)
        {
            var cartRepo = new CartRepository();
            var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            if (activeCart != null)
            {
                var cartDetail = new CartDetailRepository();
                _consoleHelper.Write($"Your order total is ${cartDetail.GetCartPrice(activeCart.CartId)}. Ready to purchase\n");
                _consoleHelper.Write("Y/N > ");
                var userInput = _consoleHelper.ReadKey();
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
                    _consoleHelper.WriteLine("Choose payment type by number>\n");

                    // read userinput
                    var paymentId = Convert.ToInt32(Console.ReadLine());

                    // update order active to false
                    cartRepo.EditCartStatus(activeCart.CartId, paymentId);
                    _consoleHelper.WriteLine("Your order is complete! Press any key to return to main menu.\n");
                    Console.ReadKey();
                    return;
                }
                return;
            }
            _consoleHelper.WriteLine("Please add some products to your order first. Press any key to return to main menu.\n");
            Console.ReadKey();
        }
    }
}
