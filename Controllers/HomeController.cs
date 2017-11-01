namespace Webshop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Webshop.Models;
    using Webshop.Models.DbXtensions;
    using Webshop.Utils.Xtratypes;
    using Webshop.Utils.Xtensions;

    [Route("/")] [Route("/[controller]")]
    public class HomeController : Controller
    {
        private WebshopContext Context; 
        public HomeController(WebshopContext context){
            this.Context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            if(Request.Cookies["user"] != null) {
                ViewData["user"] = Request.Cookies["user"];
                ViewData["username"] = Request.Cookies["username"];
                }
            //Product product = new Product{};
            //this.Context.Add(product);
            //this.Context.SaveChanges();

            return View(this.Context.SelectAllProducts().GetPage(0,3,p=>p.Id).Items.ToList());
        }

        
        [HttpGet("[action]")]
        public IActionResult About()
        {
            if(Request.Cookies["user"] != null) {
                ViewData["user"] = Request.Cookies["user"];
                ViewData["username"] = Request.Cookies["username"];
                }
            return View();
        }
  
        [HttpGet("/[action]")]
        public IActionResult Contact()
        {
            if(Request.Cookies["user"] != null) {
                ViewData["user"] = Request.Cookies["user"];
                ViewData["username"] = Request.Cookies["username"];
                }
            return View();
        }
    }
}
