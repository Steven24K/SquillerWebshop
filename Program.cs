using System;
using SquillerWebshop.Models.Opperations;
using SquillerWebshop.Models;

namespace SquillerWebshop
{
    class Program
    {
        static void Main(string[] args)
        {
           Random random = new Random();
        
           //DbOpperations.AddProdcutToShoppingCard(3,3);
           DbOpperations.SelectCustomersWithProducts();
           
        }
    }
}
