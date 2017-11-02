namespace Webshop.Controllers
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Session;
    using Webshop.Models;
    using Webshop.Models.DbXtensions;
    using Webshop.Utils.Xtratypes;
    using Webshop.Utils.Xtensions;

    [Route("/[controller]")]
    public class AccountController : Controller
    {
        private WebshopContext Context; 

        public AccountController(WebshopContext context){
            this.Context = context;
        }

        [HttpGet("[action]")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("Email, Password")] ApplicationUser user)
        {
            if(ModelState.IsValid)
            {
                if(this.Context.CheckLoginCredentials(user.Email, user.Password))
                {
                    //Make a Session to login, wich holds the user Id
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(1);
                    int id = this.Context.SelectCustomerIdByEmail(user.Email);
                    Customer customer = this.Context.SelectCustomerById(id);
                    Response.Cookies.Append("user", id.ToString(),options );
                    Response.Cookies.Append("username", customer.Name,options);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("[action]")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("user");
            Response.Cookies.Delete("username");
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("[action]")]
        public IActionResult ChangePassword()
        {
            if(Request.Cookies["user"] != null)
            {
                 ViewData["user"] = Request.Cookies["user"];
                 ViewData["username"] = Request.Cookies["username"];
                return View();
            }
            return RedirectToAction("Error403","Error");
        }

        [HttpPost("[action]")]
        public IActionResult ChangePassword([Bind("Password, NewPassword")] ChangePassword change)
        {
             if(Request.Cookies["user"] != null) 
             {
                 Customer c = this.Context.SelectCustomerById(Convert.ToInt32(Request.Cookies["user"]));
                 if(this.Context.CheckLoginCredentials(c.Email, change.Password))
                 {
                    c.Password = change.NewPassword;
                    this.Context.SaveChanges();
                    return RedirectToAction("Detail","Customer", new {id = Convert.ToInt32(Request.Cookies["user"])});
                 }
                 return RedirectToAction(nameof(ChangePassword));
             }
             return RedirectToAction("Error403","Error");
        }

    }
}
