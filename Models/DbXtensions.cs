namespace Webshop.Models.DbXtensions
{
    using System;
    using System.Linq;
    using System.Collections.Generic;


    using Webshop.Utils.Xtratypes;
    using Webshop.Models;
    using Webshop.Models.EntityInfo;
    using Webshop.Models.ViewModels;
    using Webshop.Utils.Xtensions;
    using Webshop.Utils.ImageProvider;

    public static class DbXtensions
    {
        public static IEnumerable<OrderViewModel> SelectOrderByCustomerId(this WebshopContext db, int customerId)
        {
            return (from orders in db.SelectAllOrders()
                    where orders.CustomerId == customerId
                    select orders);
        }
        public static IEnumerable<OrderViewModel> SelectAllOrders(this WebshopContext db)
        {
            return (from order in db.Orders
                    from po in db.ProductOrders
                    from customer in db.Customers
                    from product in db.Products
                    where customer.Id == order.CustomerId && product.Id == po.ProductId && po.OrderId == order.Id
                    select new OrderViewModel{
                        OrderId = order.Id,
                        ProductId = product.Id,
                        Product = product.Name,
                        Status = order.Status,
                        paymentMethod = order.paymentMethod,
                        Payed = order.Payed,
                        CustomerId = customer.Id,
                        Customer = customer.Name + " " + customer.Surname,
                        OrderDate = order.OrderDate
                    }).ToList();
        }
        public static CreateOrderViewModel SelectOrderDetails(this WebshopContext db, int CustomerId)
        {
            return (from customer in db.Customers
                    let shoppingcart = db.SelectItemsInBasket(CustomerId)
                    let TotalPrice = db.Price2Pay(CustomerId)
                    where customer.Id == CustomerId
                    select new CreateOrderViewModel{
                        CustomerId = customer.Id,
                        Street = customer.Street,
                        PostalCode = customer.PostalCode,
                        City = customer.City,
                        Products = shoppingcart,
                        TotalPrice = TotalPrice
                    }).FirstOrDefault();

        }
        public static IEnumerable<ShoppingCart> SelectItemsInBasketFromCustomer(this WebshopContext db, int customerId)
        {
            return(from sc in db.ShoppingCart
                   where sc.CustomerId == customerId
                   select sc);
        }

        public static IEnumerable<Wishlist> SelectItemsInWishlistFromCustomer(this WebshopContext db, int customerId)
        {
            return(from wl in db.Wishlist
                   where wl.CustomerId == customerId
                   select wl);
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

        public static bool IsInWishlist(this WebshopContext db, int customerId, int productId)
        {
            if((from wl in db.Wishlist
                where wl.CustomerId == customerId && wl.ProductId == productId
               select wl).Count() == 0){
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

        public static Wishlist SelectWishlistbyCustomerProduct(this WebshopContext db, int customerId, int productId)
        {
            return(from wl in db.Wishlist
                   where wl.CustomerId == customerId && wl.ProductId == productId
                   select wl).FirstOrDefault();
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

        public static IEnumerable<ProductViewModel> SelectProductsWithImage(this WebshopContext db, string keyword = null, string order_by = "TIME")
        {
            return (from p in db.Products
                    select new ProductViewModel{
                           Id = p.Id,
                           Name = p.Name,
                           Description = p.Description,
                           Category = p.Category,
                           Brand = p.Brand,
                           Price = p.Price,
                           Gender = p.Gender,
                           Extra = p.Extra,
                           Amount = p.Amount,
                           DateAdded = p.DateAdded,
                           ImageUrl = ImageCollector.GetUrls(ImageCollector.GetHtmlCode(p.Name)).ToArray()[0] ?? ImageCollector.GetUrls(ImageCollector.GetHtmlCode("No Image")).ToArray()[0]
                    });
        }

        public static IEnumerable<Product> SelectAllProducts(this WebshopContext db, string keyword = null ,string order_by = "TIME", Gender gender = Gender.ALL){
            var res = (from product in db.Products
                   select product
                   );

            if(keyword != null)res = from p in res 
                                     where p.Name.ToLower().Contains(keyword) || p.Description.ToLower().Contains(keyword)
                                     select p;

            //Make more filter functions...
            //TODO:
            //...

            if(gender != Gender.ALL) res = from p in res
                                           where p.Gender == gender
                                           select p;
            //Return one output, maybe

            switch(order_by){
                case "NAME":
                     return (from p in res orderby p.Name ascending select p).ToList();
                case "PRICELOWHIGH":
                      return (from p in res orderby p.Price ascending select p).ToList();
                case "PRICEHIGHLOW":
                      return (from p in res orderby p.Price descending select p).ToList();
                case "AMOUNT":
                      return (from p in res orderby p.Amount ascending select p).ToList();
                case "TIME":
                     return (from p in res orderby p.DateAdded ascending select p).ToList();
                default:                    
                    return res.ToList();
            }
        }

        public static Product SelectProductById(this WebshopContext db, int id)
        {
            return (from product in db.Products
                    where product.Id == id
                    select product ).FirstOrDefault();
        }

        public static Product SelectProductByCategory(this WebshopContext db, string keyword)
        {
            return (from product in db.Products
                    where product.Category == keyword
                    select product ).FirstOrDefault();
        }

        public static Customer SelectCustomerById(this WebshopContext db,int id)
        {
            return (
                from customer in db.Customers
                where customer.Id == id
                select customer).FirstOrDefault();
        }

        public static IEnumerable<Customer> SelectAllCustomers(this WebshopContext db,string order ,string keyword = null)
        {
            var res = (from customer in db.Customers
                      select customer);
                      
            if(keyword != null)res = from c in res 
                                     where (c.Name + " " + c.Surname).ToLower().Contains(keyword.ToLower()) 
                                     select c;
             switch(order)
             {
                case "TIME":
                      return (from c in res orderby c.RegistrationDate ascending select c).ToList();
                case "NAME":
                       return (from c in res orderby c.Name ascending select c).ToList();
                case "SURNAME":
                      return (from c in res orderby c.Surname ascending select c).ToList();
                case "GENDER":
                      return (from c in res orderby c.Gender ascending select c).ToList();
                case "EMAIL":
                      return (from c in res orderby c.Email ascending select c).ToList();
                case "STREET":
                      return (from c in res orderby c.Street ascending select c).ToList();
                case "POSTALCODE":
                      return (from c in res orderby c.PostalCode ascending select c).ToList();
                case "CITY":
                      return (from c in res orderby c.City ascending select c).ToList();
                default:
                      return res;
             }

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