using BangazonTerminalInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BangazonTerminalInterface.Interfaces
{
    interface IProduct
    {
        // Read
        Product GetProductById(int productId);
        //List<Product> GetAllProducts();
    }
}
