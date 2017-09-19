using System;
using SquillerWebshop.Models.Opperations;
using SquillerWebshop.Models;

namespace SquillerWebshop
{
    class Program
    {
        static void Main(string[] args)
        {
        //Do not delete these Opperations, this will tell you what is inserted into the database
           //ID:1
           //DbOpperations.AddProduct("IPhone X","It is just an to expensive Phone, but is an IPHONE!!!","phone","apple",1199.95,Gender.UNSPECIFIED,Extra.EXTRAVAGANT,10000);
           //ID:2 
           //DbOpperations.AddProduct("Golden Rolex","Nice and shiny Gold!", "watches","rolex",800,Gender.UNSPECIFIED,Extra.LIMITED,1000);
           //ID:3 
           //DbOpperations.AddProduct("Gucci Polo white", "Just a wite shirt, way to expensive", "shirts","gucci",300,Gender.MAN,Extra.REGULAR,5000);
           //ID:4 
           //DbOpperations.AddProduct("Gucci Polo white", "Just a wite shirt, way to expensive", "shirts","gucci",300,Gender.WOMEN,Extra.REGULAR,5000);
           //ID:5
           //DbOpperations.AddCustomer("Steven","Koerts",Gender.MAN,"jouwnaam@asdasd.nl","Zegikniet123!","Straatweg 13","4432FU","Schiedam");
           //ID:6 
           //DbOpperations.AddCustomer("Henk","Schoenmaker",Gender.COMPLICATED,"henk@henk.nl","wachtwoord","kleiweg 45","5235FR","Lutjebroek");
          

           DbOpperations.AddProductToShoppingCard(5,14);//Only if the relation does not exist it will add the product
           foreach(var product in DbOpperations.SelectProductsByCustomer(5)){
               System.Console.WriteLine(product.Name + " " + DbOpperations.SelectAmountofItems(5,14).Visit<string>(sc => sc.Amount.ToString(),()=>"0"));
           }

        }
    }
}
