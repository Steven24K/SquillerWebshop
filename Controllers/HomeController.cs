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
        private CookieOptions Options;
        public HomeController(WebshopContext context){
            this.Context = context;
            this.Options = new CookieOptions{Expires = DateTime.Now.AddYears(10)};
        }
        [HttpGet]
        public IActionResult Index()
        {
            if(Request.Cookies["comeBack"] == null){
                Response.Cookies.Append("comeBack","I was here!", Options);
            }
            
            //To add a product to the database
            // Product product = new Product{
            //       Name = "Rolex",
            //       Description = "Nice and shiny",
            //       Category = "Watch",
            //       Brand = "Rolex",
            //       Price = 10000,
            //       Gender = Gender.UNSPECIFIED,
            //       Extra = Extra.LIMITED,
            //       Amount = 10
            // };
            
            //this.Context.Add(product);
            //this.Context.SaveChanges();

            return View(this.Context.SelectAllProducts().GetPage(0,3,p=>p.Id).Items.ToList());
        }

        
        [HttpGet("[action]")]
        public IActionResult About()
        {
            return View();
        }
  
        [HttpGet("[action]")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Team()
        {
            return View();           
        }

        [HttpGet("[action]")]
        public IActionResult Terms()
        {
            return View();           
        }

        [HttpGet("[action]")]
        public IActionResult Video()
        {
            return View();           
        }

        [HttpGet("[action]")]
        public IActionResult FAQ()
        {
            return View();           
        }
    }
}
