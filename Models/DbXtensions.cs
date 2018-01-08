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

    public static class DbXtensions
    {
        public static OrderDetailsView SelectOrderByCustomerId(this WebshopContext db, int customerId, int orderId)
        {
            return (from order in db.Orders
                    from po in db.ProductOrders
                    let products = (from p in db.Products
                                    from productorder in db.ProductOrders
                                    where p.Id == productorder.ProductId && productorder.OrderId == orderId
                                    select new AmountProduct{
                                        Id = p.Id, 
                                        Name = p.Name,
                                        Price = p.Price,
                                        Amount = productorder.Amount
                                        }).ToList()
                    where order.Id == po.OrderId && order.CustomerId == customerId && order.Id == orderId
                    select new OrderDetailsView{
                        OrderId = order.Id,
                        Status = order.Status,
                        paymentMethod = order.paymentMethod,
                        Payed = order.Payed,
                        CustomerId = order.CustomerId,
                        Products = products,
                        TotalPrice = products.Sum(p => p.Amount * p.Price),
                        OrderDate = order.OrderDate
                    }).FirstOrDefault();
        }
        public static IEnumerable<OrderViewModel> SelectAllOrders(this WebshopContext db, OrderStatus orderStatus = OrderStatus.ALL, int id = -1)
        {
            var orders = (from order in db.Orders
                    from customer in db.Customers
                    let Price = (from po in db.ProductOrders
                                 from product in db.Products
                                 where po.OrderId == order.Id && po.ProductId == product.Id
                                 select po.Amount * product.Price).Sum()
                    where customer.Id == order.CustomerId 
                    select new OrderViewModel{
                        OrderId = order.Id,
                        Status = order.Status,
                        paymentMethod = order.paymentMethod,
                        Payed = order.Payed,
                        CustomerId = customer.Id,
                        Customer = customer.Name + " " + customer.Surname,
                        TotalPrice = Price,
                        OrderDate = order.OrderDate
                    });

            //This to only select the orders from a specific customer, to prevent that a random customer can view orders
            //from other customers.
            if(id >= 0)return (from o_byid in orders
                              where o_byid.CustomerId == id
                              select o_byid).ToList();

            if(orderStatus != OrderStatus.ALL){
                orders = (from filtered_orders in orders
                         where filtered_orders.Status == orderStatus
                         select filtered_orders);
            }
            return orders.ToList();
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

        public static IEnumerable<Product> SelectAllProducts(this WebshopContext db, string keyword = null ,string order_by = "TIME", Gender gender = Gender.ALL, string category = null, Extra extra = Extra.ALL)
        {
            //Select all products
            var res = (from product in db.Products
                   select product
                   );

            //From here we reduce the products according to the funtions parameters
            
            if(keyword != null){ 
                keyword = keyword.ToLower(); //Make lower case letters for a more fair search and better comparison
                res = from p in res 
                      where p.Name.ToLower().Contains(keyword) || p.Description.ToLower().Contains(keyword)
                      select p;
            }

            //Filter by genders
            if(gender != Gender.ALL) res = from p in res
                                           where p.Gender == gender
                                           select p;
            //Filter by category
            if(category != null){
                 res = from p in res 
                       where p.Category.ToLower().Contains(category) | p.Category.ToLower() == category.ToLower()
                       select p;
            }
            
            //Filter by extra's
            if(extra != Extra.ALL) res = from p in res 
                                         where p.Extra == extra
                                         select p;

            switch(order_by){
                case "NAME":
                     return (from p in res orderby p.Name ascending select p).ToList();
                case "CATEGORY":
                     return (from p in res orderby p.Category ascending select p).ToList();
                case "BRAND":
                     return (from p in res orderby p.Brand ascending select p).ToList();
                case "PRICELOWHIGH":
                      return (from p in res orderby p.Price ascending select p).ToList();
                case "PRICEHIGHLOW":
                      return (from p in res orderby p.Price descending select p).ToList();
                case "GENDER":
                      return (from p in res orderby p.Gender ascending select p).ToList();
                case "AMOUNT":
                      return (from p in res orderby p.Amount ascending select p).ToList();
                case "EXTRA":
                      return (from p in res orderby p.Extra ascending select p).ToList();
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

        public static Customer SelectCustomerById(this WebshopContext db,int id)
        {
            return (
                from customer in db.Customers
                where customer.Id == id
                select customer).FirstOrDefault();
        }

        public static IEnumerable<Customer> SelectAllCustomers(this WebshopContext db, string keyword = null, string order = null)
        {
            //Select all customers
            var res = (from customer in db.Customers
                      select customer);
                      
            //From here we start reducing the result
            //If keyword is not null filter all the customers wich contains the same letters as the keyword, 
            //This being compared in keyword and name+surname comparison
            //SELECT * FROM Customers
            //WHERE (Name+Surname) LIKE %keyword%
            if(keyword != null)res = from c in res 
                                     where (c.Name + " " + c.Surname).ToLower().Contains(keyword.ToLower()) 
                                     select c;

            //This switch statement depends the order of the customers
             switch(order)
             {
                case "TIME":
                      return (from c in res orderby c.RegistrationDate descending select c).ToList();
                case "NAME":
                       return (from c in res orderby c.Name ascending select c).ToList();
                case "SURNAME":
                      return (from c in res orderby c.Surname ascending select c).ToList();
                case "GENDER":
                      return (from c in res orderby c.Gender descending select c).ToList();
                case "EMAIL":
                      return (from c in res orderby c.Email ascending select c).ToList();
                case "STREET":
                      return (from c in res orderby c.Street ascending select c).ToList();
                case "POSTALCODE":
                      return (from c in res orderby c.PostalCode descending select c).ToList();
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

        public static IEnumerable<Product> SelectRecommendedProducts(this WebshopContext db, Product selectedProduct)
        {   
            //The recommendation algorithm, based on the order history from other customers
            var result = (from products in db.Products              
                          let recommendedProductIds = (from productorder in db.ProductOrders
                                                       let orderIds = (from po in db.ProductOrders
                                                                       where po.ProductId == selectedProduct.Id
                                                                       select po.OrderId)
                                                       where orderIds.Contains(productorder.OrderId) && productorder.ProductId != selectedProduct.Id
                                                       select productorder.ProductId).Distinct()
                           where recommendedProductIds.Contains(products.Id)
                           select products);
            
            //In case there is no history mathching the result we select the recommended products on there category or brand
            if(result == null | result.Count() == 0)
            {
                result = (from product in db.Products
                          where product.Id != selectedProduct.Id && (product.Brand.ToLower().Contains(selectedProduct.Brand.ToLower()) |
                                     product.Category.ToLower().Contains(selectedProduct.Category.ToLower()))
                          select product);
            }

            //In case that is there is still no mathching result, we just select the latest added products from the database
            if(result == null | result.Count() == 0)
            {
                result = (from product in db.Products
                          where product.Id != selectedProduct.Id
                          orderby product.DateAdded descending
                          select product);
            }

            return result.ToList();
        }
    }
}