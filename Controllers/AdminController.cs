namespace Webshop.Controllers
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

    using Webshop.Models;
    using Webshop.Models.DbXtensions;
    using Webshop.Models.EntityInfo;
    using Webshop.Utils.Xtratypes;
    using Webshop.Utils.Xtensions;
     
    [Route("/[controller]")]
    public class AdminController : Controller
    {
        private readonly WebshopContext Context;
        public AdminController(WebshopContext context){this.Context = context;}

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            TempData["comeBack"] = Request.Cookies["comeBack"];
            //Adds a new admin if there is no admin in the database
            if(Context.Administrators.Count() == 0){
            Context.Add(new Administrator{
                UserName = "admin",
                Password = "admin",
                Email = "admin@example.com"
                });
            Context.SaveChanges();
            }

            //To make sure the user never sees the admin page
            if(Request.Cookies["user"] != null){return RedirectToAction("Error403","Error");}

            if(Request.Cookies["admin"]==null)return View();
            return RedirectToAction(nameof(Options));
        }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserName, Password")] Administrator admin)
        {
            if(ModelState.IsValid && this.Context.CheckAdmin(admin.UserName, admin.Password)){
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddYears(1000);
                Response.Cookies.Append("admin",admin.UserName,options);
                return RedirectToAction(nameof(Options));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]")]
        public IActionResult Logout()
        {
            TempData["comeBack"] = Request.Cookies["comeBack"];
            Response.Cookies.Delete("admin");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]")]
        public IActionResult Options()
        {
            TempData["comeBack"] = Request.Cookies["comeBack"];
            
            if(Request.Cookies["admin"] != null){
                ViewData["admin"] = Request.Cookies["admin"];
                return View();
                }
            return RedirectToAction("Error403", "Error");
        }
    }
}