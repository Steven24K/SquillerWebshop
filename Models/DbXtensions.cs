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
        public static IEnumerable<ShoppingCart> SelectItemsInBasketFromCustomer(this WebshopContext db, int customerId)
        {
            return(from sc in db.ShoppingCart
                   where sc.CustomerId == customerId
                   select sc);
        }
        public static bool CheckStock(this WebshopContext db, int productId)
        {
            if((from p in db.Products
               where p.Id == productId
               select p.Amount).FirstOrDefault() > 0){return true;}
               else{
                   return false;
               }
        }
        public static ShoppingCart SelectShoppingCartItem(this WebshopContext db, int productId, int customerId)
        {
            return(from sc in db.ShoppingCart
                   where sc.CustomerId == customerId && sc.ProductId == productId
                   select sc).FirstOrDefault();
        }
        public static StockInicator IsinStock(this WebshopContext db, int productId)
        {
            int product = (from p in db.Products
                           where p.Id == productId
                           select p.Amount).FirstOrDefault();
            if(product <=0)return StockInicator.OUTOFORDER;
            if(product <=5)return StockInicator.LESSTHANFIVE;
            return StockInicator.PLENTY;
        }

        public static double Price2Pay(this WebshopContext db, int customerId)
        {
             var items = (from sc in db.ShoppingCart
                          from p in db.Products
                          where sc.CustomerId == customerId && sc.ProductId == p.Id 
                          select new {
                              Price = p.Price * sc.Amount
                          });


             double Total = 0;
              foreach (var item in items)
              {
                  Total += item.Price;
              } 
              return Total;
        }

        public static IEnumerable<ShoppingCartView> SelectItemsInBasket(this WebshopContext db, int customerId)
        {
            return(from sc in db.ShoppingCart
                   from p in db.Products
                   where sc.CustomerId == customerId && sc.ProductId == p.Id
                   orderby p.Name descending
                   select new ShoppingCartView{
                       Id = sc.ProductId,
                       ProductName = p.Name,
                       Amount = sc.Amount,
                       Price = p.Price.FormatPrice(),
                       TotalPrice = (p.Price * sc.Amount).FormatPrice()
                   });
        }
        public static bool IsInBasket(this WebshopContext db, int customerId, int productId)
        {
            if((from sc in db.ShoppingCart
               where sc.CustomerId == customerId && sc.ProductId == productId
               select sc).Count() == 0){
                   return false;
               }else{
                   return true;
               }

        }

        public static ShoppingCart SelectBasketbyCustomerProduct(this WebshopContext db, int customerId, int productId)
        {
            return(from sc in db.ShoppingCart
                   where sc.CustomerId == customerId && sc.ProductId == productId
                   select sc).FirstOrDefault();
        }
        public static bool CheckAdmin(this WebshopContext db,string username, string password)
        {
            if((from admin in db.Administrators
                 where admin.UserName == username && admin.Password == password
                 select admin).Count() == 0)return false;
                 return true;
        }
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

        public static IEnumerable<Product> SearchCategory(this WebshopContext db, int keyword)
        {
            return (
                from product in db.Products
                where product.Gender.Equals(keyword)
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