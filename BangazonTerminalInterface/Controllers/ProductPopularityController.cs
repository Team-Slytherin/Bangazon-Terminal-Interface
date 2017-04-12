using BangazonTerminalInterface.DAL.Repository;
using BangazonTerminalInterface.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Controllers
{
    class ProductPopularityController
    {

        ProductRepository repo = new ProductRepository();

        ConsoleHelper _consoleHelper;

        public ProductPopularityController()
        {
            _consoleHelper = new ConsoleHelper();
        }
        public void DisplayPopularProducts()
        {
            char spacePad = ' ';
            _consoleHelper.WriteHeaderToConsole("Product Popularity Report");
            _consoleHelper.WriteLine("Product           Orders     Customers  Revenue          ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;

            var popularProducts = repo.GetProductPopularity();
            foreach (var product in popularProducts)
            {
                _consoleHelper.WriteLine(product.ProductName.PadRight(18, spacePad).Substring(0, 17) + spacePad + product.Orders.ToString().PadRight(11, spacePad).Substring(0, 11) + product.Customers.ToString().PadRight(11, spacePad).Substring(0, 11) + "$" + product.Revenue);
            }
            decimal totalOrders = popularProducts.Sum(item => item.Orders);
            decimal totalCustomers = popularProducts.Sum(item => item.Customers);
            decimal totalRevenue = popularProducts.Sum(item => item.Revenue);

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            _consoleHelper.WriteLine("*********************************************************");
            Console.ForegroundColor = ConsoleColor.White;
            _consoleHelper.WriteLine("Totals:           " + totalOrders.ToString().PadRight(11, spacePad).Substring(0, 10) + spacePad + totalCustomers.ToString().PadRight(11, spacePad).Substring(0, 11) + "$" + totalRevenue.ToString());
            _consoleHelper.WriteLine("Press any key to return to main menu");
            _consoleHelper.ReadKey();
        }
    }
}
