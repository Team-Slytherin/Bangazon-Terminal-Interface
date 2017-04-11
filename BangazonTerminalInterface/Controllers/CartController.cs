using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Helpers;
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
        public void addProduct(Customer activeCustomer)
        {
            SHOWPRODUCTS:
                Console.Clear();
                ProductRepository repo = new ProductRepository();

                var products = repo.GetAllProducts();
                foreach (Product product in products)
                {
                    Console.WriteLine(product.ProductId + ". " + product.ProductName + "\n");
                }
                Console.WriteLine($"{products.Count + 1}" + ". Save order and back to main menu\n");
                Console.WriteLine($"{products.Count + 2}" + ". Checkout\n");
                try
                {
                    var selectedProduct = Convert.ToInt32(Helper.WriteToConsole("> "));
                
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
                        Console.WriteLine("One item has been put into your cart.");
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
                        Console.WriteLine("Please choose a valid product number!");
                        goto SHOWPRODUCTS;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Please enter the numbers showed on screen!");
                    Thread.Sleep(1000);
                    goto SHOWPRODUCTS;
                }
        }

        public void checkout(Customer activeCustomer)
        {
            var cartRepo = new CartRepository();
            var activeCart = cartRepo.GetActiveCart(activeCustomer.CustomerId);
            if (activeCart != null)
            {
                var cartDetail = new CartDetailRepository();
                Console.Write($"Your order total is ${cartDetail.GetCartPrice(activeCart.CartId)}. Ready to purchase\n");
                var userInput = Helper.WriteToConsole("Y/N > ");
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

                    // read userinput
                    var paymentId = Convert.ToInt32(Helper.WriteToConsole("Choose payment type by number >\n"));

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
