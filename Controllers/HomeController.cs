namespace Webshop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
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
            return View(this.Context.BindSelectAllProducts().GetPage(0,3,p=>p.Id).Items.ToList());
        }

        [HttpGet("[action]")]
        public IActionResult About()
        {
            return View();
        }
  
        [HttpGet("/[action]")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
