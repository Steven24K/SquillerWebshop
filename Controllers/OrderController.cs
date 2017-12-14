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

    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private WebshopContext Context; 
        public OrderController(WebshopContext context){this.Context = context;}

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            if(this.HttpContext.Request.Cookies["admin"] != null)return View(this.Context.SelectAllOrders());
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

        [HttpGet("[action]")]
        public IActionResult Edit()
        {
            throw new NotImplementedException("Editen van de order komt nog.");
        }

        [HttpGet("[action]")]
        public IActionResult Cancel()
        {
            throw new NotImplementedException("Het cancelen van een order moet nog gebouwd worden, kan alleen als een order nog niet verzonden is of betaald.");
        }
    }
}