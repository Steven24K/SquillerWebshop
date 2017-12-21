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
    public class WishlistController : Controller
    {
        private WebshopContext Context;
        public WishlistController(WebshopContext context){
             this.Context = context;
        }

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            //Check if user is logged in
            if(Request.Cookies["user"] != null) {
                int userId = Convert.ToInt32(Request.Cookies["user"]);
                
                 ViewData["TotalPrice"] = this.Context.Price2Pay(userId).FormatPrice();
                 return View(this.Context.SelectItemsInBasket(userId));
                }
            return RedirectToAction("Error403","Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost("[action]")]
        public IActionResult Add([Bind("Id, CustomerId, Amount")] WishlistView wishList)
        {
           if(ModelState.IsValid) //&& this.Context.CheckStock(shoppingCart.Id))
           {
               if(this.Context.IsInWishlist(wishList.CustomerId,wishList.Id))
               {
                    Wishlist wl = this.Context.SelectWishlistbyCustomerProduct(wishList.CustomerId,wishList.Id);
                    wl.Amount += wishList.Amount;
               }else{
               this.Context.Add(new Wishlist{
                   CustomerId = wishList.CustomerId,
                   ProductId = wishList.Id,
                   Amount = wishList.Amount
               });
               }
               Product p = this.Context.SelectProductById(wishList.Id);
               p.Amount -= wishList.Amount;
               this.Context.SaveChanges();
           }
           return RedirectToAction(nameof(Index));
        }

                [HttpGet("[action]")]
        public IActionResult DeleteAll()
        {
            if(Request.Cookies["user"] != null){
                foreach(var s in this.Context.SelectItemsInWishlistFromCustomer(Convert.ToInt32(Request.Cookies["user"]))){
                this.Context.Remove(s);
                Product p = this.Context.SelectProductById(s.ProductId);
                p.Amount += s.Amount;
                }
                this.Context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Error403","Error");
        }
    }
}