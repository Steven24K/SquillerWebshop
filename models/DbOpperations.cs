using System.Linq;
using System.Collections.Generic;

using SquillerWebshop.DesignPatterns;

namespace SquillerWebshop.Models.Opperations
{
    public class DbOpperations
    {

        public static void AddProduct(string name,string desription, string category,string brand, double price, Gender gender, Extra extra, int amount)
        {
           using(var db = new WebshopContext())
           {
               Product p = new Product()
               {
                   Name = name,
                   Description = desription,
                   Category = new Category{Name = category.ToUpper()},
                   Brand = new Brand{Name = brand.ToUpper()},
                   Price = price,
                   Gender = gender,
                   Extra = extra,
                   Amount = new Inventory{Amount = amount}
               };
               db.Products.Add(p);
               db.SaveChanges();
           }
        }

        private static bool RelationExist(int customer_id, int ProductId)
        {
            using (var db = new WebshopContext()){

                var query = from shoppingcart in db.ShoppingCart
                            where shoppingcart.CustomerId == customer_id && shoppingcart.ProductId == ProductId
                            select shoppingcart;
                if(query.Count() < 1)return false;
                return true;
            }
        }
        public static void AddProductToShoppingCard(int customer_id, int product_id)
        {
            using(var db = new WebshopContext())
            {
                //There must be a check to verify that the realtion already exists or not
                //Also add a amount attribute to the shoppingCard to see how much someone bought 
                //of a particular product.
             if(!DbOpperations.RelationExist(customer_id,product_id)){
               ShoppingCart sc = new ShoppingCart
               {
                   CustomerId = customer_id,
                   ProductId = product_id,
                   Amount = 1
               };
               db.ShoppingCart.Add(sc);
               var query = from i in db.Inventory
                           where i.Id == product_id
                        select i.Amount;
                query.ToArray()[0] -= 1;
               db.SaveChanges();
                }
                else{
                    var query = from sc in db.ShoppingCart
                                where sc.CustomerId == customer_id && sc.ProductId == product_id
                                select sc.Amount;
                    int first = query.ToArray()[0];
                    query.ToArray()[0] += 1;
                    int seccond = query.ToArray()[0];
                     db.SaveChanges();
                }
            }
        }
        public static void AddCustomer(string name, string surname, Gender gender, string email, string password, string adress, string postalcode, string city)
        {
            using(var db = new WebshopContext())
            {

                Customer c = new Customer{
                    Name = name,
                    Surname = surname,
                    Gender = gender,
                    Email = email,
                    Password = password,
                    Adress = adress,
                    PostalCode = postalcode,
                    City = city
                 };
             db.Customers.Add(c);
             db.SaveChanges();
            }
        }

        public static Customer[] SelectAllCustomers()
        {
            using(var db = new WebshopContext()){
            
               var q0 = from customer in db.Customers
                        select customer;
            
             return q0.ToArray();
            }
        }

        public static Product[] SelectAllProducts()
        {
            using(var db = new WebshopContext()){
            
               var q0 = from product in db.Products
                        select product;
            
             return q0.ToArray();
            }
        }

        public static Customer SelectCustomerById(int Id)
        {
            using(var db = new WebshopContext())
            {
                var query = from customer in db.Customers
                            where customer.Id == Id
                            select customer;
                return query.First();
            }
        }

        public static Product SelectProductById(int Id)
        {
            using(var db = new WebshopContext())
            {
                var query = from product in db.Products
                            where product.Id == Id
                            select product;
                return query.First();
            }
        }

        public static void SelectCustomersWithProducts()
        {
            using(var db = new WebshopContext())
            {
                var query = from c in db.Customers
                            from p in db.Products
                            from shoppingcart in db.ShoppingCart
                            where shoppingcart.CustomerId == c.Id && p.Id == shoppingcart.ProductId
                            select new {
                                customer = c.Name + " " + c.Surname,
                                product = p.Name
                            };
              foreach(var res in query){
                  System.Console.WriteLine(res.customer + " " + res.product);
              }
            }
        }

        public static Product[] SelectProductsByCustomer(int id){
            using (var db = new WebshopContext()){
                var query = from p in db.Products
                            from c in db.Customers
                            from sc in db.ShoppingCart
                            where sc.CustomerId == c.Id && sc.ProductId == p.Id && sc.CustomerId == id
                            select p;
                return query.ToArray();
            }
        }

        public static IOption<ShoppingCart> SelectAmountofItems(int c_id, int p_id){
            using(var db = new WebshopContext()){
                   if(DbOpperations.RelationExist(c_id,p_id)){
                       var query = from sc in db.ShoppingCart
                                   where sc.CustomerId == c_id && sc.ProductId == p_id
                                   select sc;
                     return new Some<ShoppingCart>(query.First());
                   }
                   return new None<ShoppingCart>();
            }
        }

        public static void ChangeInventory(int FK_inventoryId, int n){
            using (var db = new WebshopContext()){
                var query = from inventory in db.Inventory
                            where inventory.Id == FK_inventoryId
                            select inventory;
                query.First().Amount -= n;
                db.SaveChanges();
            }
        }
    }
}