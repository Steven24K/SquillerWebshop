namespace Webshop.Models.DbXtensions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;


    using Webshop.Utils.Xtratypes;
    using Webshop.Models;
    using Webshop.Models.EntityInfo;
    using Webshop.Utils.Xtensions;

    public static class DbXtensions
    {
        public static int SelectCustomerIdByEmail(this WebshopContext db, string email)
        {
            return (from customer in db.Customers
                    where customer.Email == email
                    select customer.Id).FirstOrDefault();
        }
        public static IEnumerable<Product> SelectAllProducts(this WebshopContext db){
            return (from product in db.Products
                   orderby product.DateAdded descending
                   select product
                   ).ToList();
        }

        public static Product SelectProductById(this WebshopContext db, int id)
        {
            return (from product in db.Products
                    where product.Id == id
                    select product ).FirstOrDefault();
        }

        public static Customer SelectCustomerById(this WebshopContext db,int id)
        {
            return (
                from customer in db.Customers
                where customer.Id == id
                select customer).FirstOrDefault();
        }
        public static IEnumerable<Product> SearchProducts(this WebshopContext db, string keyword)
        {
            keyword = keyword.ToLower();
            return (
                from product in db.Products
                where product.Name.ToLower().Contains(keyword) || product.Description.ToLower().Contains(keyword)
                select product
            ).ToList();
        }

        public static IEnumerable<Customer> SelectAllCustomers(this WebshopContext db)
        {
            return (
                from customer in db.Customers
                select customer
            ).ToList();
        }

        public static bool CheckLoginCredentials(this WebshopContext db, string email, string password)
        {
            var x = from customer in db.Customers
                    where customer.Email == email && customer.Password == password
                    select customer;
            if(x.Count() == 0)return false;
            return true;

        }
    }
}