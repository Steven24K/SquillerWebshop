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
            TempData["comeBack"] = Request.Cookies["comeBack"];
            //Check if admin is logged in 
            if(Request.Cookies["admin"] != null){ ViewData["admin"] = Request.Cookies["admin"];}
               
            //Check if user is looged in
            if(Request.Cookies["user"] != null) {
                ViewData["user"] = Request.Cookies["user"];
                ViewData["username"] = Request.Cookies["username"];
                }
            return View(this.Context.SelectItemsInBasket(Convert.ToInt32(Request.Cookies["user"])));
        }


        [ValidateAntiForgeryToken]
        [HttpPost("[action]")]
        public IActionResult Add([Bind("ProductId, CustomerId, Amount")] ShoppingCart shoppingCart)
        {
           if(ModelState.IsValid)
           {
               if(this.Context.IsInBasket(shoppingCart.CustomerId,shoppingCart.ProductId))
               {
                    ShoppingCart sc = this.Context.SelectBasketbyCustomerProduct(shoppingCart.CustomerId,shoppingCart.ProductId);
                    sc.Amount += shoppingCart.Amount;
               }else{
               this.Context.Add(shoppingCart);
               }
               Product p = this.Context.SelectProductById(shoppingCart.ProductId);
               p.Amount -= shoppingCart.Amount;
               this.Context.SaveChanges();
           }
           return RedirectToAction(nameof(Index));
        }
    }
}