namespace Webshop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Webshop.Models;
    using Webshop.Models.EntityInfo;
    using Webshop.Models.DbXtensions;
    using Webshop.Utils.Xtratypes;
    using Webshop.Utils.Xtensions;
    using Webshop.Models.ViewModels;
    using Webshop.Utils.Paginator;

    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private WebshopContext Context; 
        public OrderController(WebshopContext context){this.Context = context;}

        [HttpGet("[action]")]
        public IActionResult Index(int page = 0, OrderStatus status = OrderStatus.ALL)
        {
            if(this.HttpContext.Request.Cookies["admin"] != null)return View(this.Context.SelectAllOrders(status).GetPage(page, 50) ?? new Page<OrderViewModel>{Index = 0, TotalPages = 1, Items = new OrderViewModel[]{}});
            return RedirectToAction("Error404", "Error");
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View(this.Context.SelectOrderDetails(Convert.ToInt32(HttpContext.Request.Cookies["user"])));
        }

        [HttpPost("[action]")]
        public IActionResult CreateOrder([Bind("CustomerId, paymentMethod")] CreateOrderViewModel order)
        {
            //Select product from the shoppingcart by CustomerId
            var shopingCart = this.Context.SelectItemsInBasketFromCustomer(order.CustomerId);

            //Wrap the result from the shoppingcart query inside a ProductOrder objectt
            List<ProductOrder> products = new List<ProductOrder>();
            foreach( var product in shopingCart){
                products.Add(new ProductOrder{
                    ProductId = product.ProductId,
                    Amount = product.Amount
                });
            }

            //Create order from View gathered by model binding
            Order new_order = new Order{
                CustomerId = order.CustomerId,
                Status = OrderStatus.TO_BE_PAYED,
                paymentMethod = order.paymentMethod,
                Payed = false,
                Products = products
            };

            //Empty shoppingcart
            foreach(var s in this.Context.SelectItemsInBasketFromCustomer(Convert.ToInt32(Request.Cookies["user"]))){
                this.Context.Remove(s);
                Product p = this.Context.SelectProductById(s.ProductId);
             }

            //SaveChanges
            this.Context.Add(new_order);
            this.Context.SaveChanges();

            //Redirect to customer detail page where the order history will be displayed
            return RedirectToAction("Detail", "Customer");
        }

        [HttpGet("[action]/{orderId}")]
        public IActionResult Detail(int orderId, int id)
        {
            if(HttpContext.Request.Cookies["user"] != null) return View(this.Context.SelectOrderByCustomerId(Convert.ToInt32(HttpContext.Request.Cookies["user"]),orderId));
            if(HttpContext.Request.Cookies["admin"] != null) return View(this.Context.SelectOrderByCustomerId(id, orderId));
            return RedirectToAction("Error403", "Error");
           
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Edit(int id)
        {
            Order o = (from orders in this.Context.Orders
                      where orders.Id == id
                      select orders).FirstOrDefault();

            return View(o);
        }

        [HttpPost("[action]")]
        public IActionResult SaveChange([Bind("Id, Status, Payed")] Order order)
        {
            Order order_2_update = (from orders in this.Context.Orders
                                   where orders.Id == order.Id
                                   select orders).FirstOrDefault();
            order_2_update.Status = order.Status;
            order_2_update.Payed = order.Payed;

            this.Context.SaveChanges();
            return RedirectToAction("Index", "Order");
        }

        [HttpGet("[action]/{orderId}")]
        public IActionResult Cancel(int orderId)
        {
            //Delete the order
            Order order_2_delete = (from orders in this.Context.Orders
                                    where orders.Id == orderId
                                    select orders).FirstOrDefault();
            this.Context.Remove(order_2_delete);

            //Put products back in the inventory
            IEnumerable<ProductOrder> productsOrdered = from po in this.Context.ProductOrders
                                            from p in this.Context.Products
                                            where po.OrderId == orderId && po.ProductId == p.Id
                                            select po;
            foreach(var product in productsOrdered){
                Product p = this.Context.SelectProductById(product.ProductId);
                p.Amount += product.Amount;
            }
            
            this.Context.SaveChanges();
            return RedirectToAction("Detail", "Customer");

        }
    }
}