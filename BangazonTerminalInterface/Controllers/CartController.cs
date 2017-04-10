using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Controllers
{
    class CartController
    {
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
                Console.Write($"Your order total is ${cartDetail.GetCartPrice(activeCart.CartId)}. Ready to purchase\n");
                Console.Write("Y/N > ");
                var userInput = Console.ReadKey(true).KeyChar.ToString();
                if (userInput.ToLower() == "y")
                {
                    // get active customer payment option and show
                    var paymentRepo = new PaymentRepository();
                    var payments = paymentRepo.GetAllPayments(activeCustomer.CustomerId);

                    Console.WriteLine("\nYour payment options:\n");
                    foreach (Payment payment in payments)
                    {
                        Console.WriteLine(payment.PaymentId + ". " + payment.PaymentType + "\n");
                    }
                    Console.WriteLine("Choose payment type by number>\n");

                    // read userinput
                    var paymentId = Convert.ToInt32(Console.ReadLine());

                    // update order active to false
                    cartRepo.EditCartStatus(activeCart.CartId, paymentId);
                    Console.WriteLine("Your order is complete! Press any key to return to main menu.\n");
                    Console.ReadKey();
                    return;
                }
                return;
            }
            Console.WriteLine("Please add some products to your order first. Press any key to return to main menu.\n");
            Console.ReadKey();
        }
    }
}
