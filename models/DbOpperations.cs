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
               db.Product.Add(p);
               db.SaveChanges();
           }
        }

        public static void AddProdcutToShoppingCard(int customer_id, int product_id)
        {
            using(var db = new WebshopContext())
            {
                //There must be a check to verify that the realtion already exists or not
                //Also add a amount attribute to the shoppingCard to see how much someone bought 
                //of a particular product.
               ShoppingCard sc = new ShoppingCard
               {
                   CustomerId = customer_id,
                   ProductId = product_id
               };
               db.shoppingCard.Add(sc);
               db.SaveChanges();
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
             db.Customer.Add(c);
             db.SaveChanges();
            }
        }

        public static Customer[] SelectAllCustomers()
        {
            using(var db = new WebshopContext()){
            
               var q0 = from customer in db.Customer
                        select customer;
            
             return q0.ToArray();
            }
        }

        public static Customer SelectCustomerById(int Id)
        {
            using(var db = new WebshopContext())
            {
                var query = from customer in db.Customer
                            where customer.Id == Id
                            select customer;
                return query.First();
            }
        }

        public static Product SelectProductById(int Id)
        {
            using(var db = new WebshopContext())
            {
                var query = from product in db.Product
                            where product.Id == Id
                            select product;
                return query.First();
            }
        }

        public static void SelectCustomersWithProducts()
        {
            using(var db = new WebshopContext())
            {
                var query = from c in db.Customer
                            from p in db.Product
                            from shoppingcart in db.shoppingCard
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
    }
}