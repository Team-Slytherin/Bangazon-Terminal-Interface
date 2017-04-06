using BangazonTerminalInterface.Components;
using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                Console.Write("Y?N>");
                var userInput = Console.ReadKey(true).KeyChar.ToString();
                if (userInput == "Y")
                {
                    // get active customer payment option and show
                    // function needed in PaymentRepository

                    // read userinput
                    var paymentId = Convert.ToInt32(Console.ReadLine());

                    // update order active to false
                    cartRepo.EditCartStatus(activeCart.CartId, paymentId);
                }
            }
        }
    }
}
