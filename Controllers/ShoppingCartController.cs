namespace Webshop.Controllers
{
    using System;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Webshop.Models;
    using Webshop.Models.DbXtensions;
    using Webshop.Utils.Xtratypes;
    using Webshop.Utils.Xtensions;

    [Route("[controller]")]
    public class ShoppingCartController : Controller
    {
        private WebshopContext Context;
        public ShoppingCartController(WebshopContext context){
             this.Context = context;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            //Check if user is looged in
            if(Request.Cookies["user"] != null) {
                int userId = Convert.ToInt32(Request.Cookies["user"]);
                //Create cookie wich contains TotalPrice
                //....
                //...
                
                 ViewData["TotalPrice"] = this.Context.Price2Pay(userId).FormatPrice();
                 return View(this.Context.SelectItemsInBasket(userId));
                }
            return RedirectToAction("Error403","Error");
        }


        [ValidateAntiForgeryToken]
        [HttpPost("[action]")]
        public IActionResult Add([Bind("Id, CustomerId, Amount")] ShoppingCartView shoppingCart)
        {
           if(ModelState.IsValid && this.Context.CheckStock(shoppingCart.Id))
           {
               if(this.Context.IsInBasket(shoppingCart.CustomerId,shoppingCart.Id))
               {
                    ShoppingCart sc = this.Context.SelectBasketbyCustomerProduct(shoppingCart.CustomerId,shoppingCart.Id);
                    sc.Amount += shoppingCart.Amount;
               }else{
               this.Context.Add(new ShoppingCart{
                   CustomerId = shoppingCart.CustomerId,
                   ProductId = shoppingCart.Id,
                   Amount = shoppingCart.Amount
               });
               }
               Product p = this.Context.SelectProductById(shoppingCart.Id);
               p.Amount -= shoppingCart.Amount;
               this.Context.SaveChanges();
           }
           return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]")]
        public IActionResult Delete(int productId){
            return View(this.Context.SelectShoppingCartItem(productId, Convert.ToInt32(Request.Cookies["user"])));
            }

        [HttpPost("[action]")]
        public IActionResult DeleteReal([Bind("CustomerId, ProductId")] ShoppingCart shoppingCart)
        {
            //TODO: When an item is deleted the amount should be added to the stock
            this.Context.Remove(shoppingCart);
            this.Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]")]
        public IActionResult DeleteAll()
        {
            if(Request.Cookies["user"] != null){
                foreach(var s in this.Context.SelectItemsInBasketFromCustomer(Convert.ToInt32(Request.Cookies["user"]))){
                this.Context.Remove(s);
                }
                this.Context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error403","Error");
        }
    }
}