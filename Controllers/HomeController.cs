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
    using Webshop.Xtratypes;

    [Route("/")] [Route("/Home")]
    public class HomeController : Controller
    {
        private WebshopContext Context; 
        public HomeController(WebshopContext context){
            this.Context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            Context.AddProduct("Ferrari","Just a nice red car","cars","ferrari",300000,Gender.MAN,Extra.EXTRAVAGANT,500 );
            ViewData["Product"] = this.Context.SelectProductById(14).Name;
            return View();
        }

          [HttpGet("/About")]
        public IActionResult About()
        {
            return View();
        }
  
        [HttpGet("/Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet("/Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/Detail")]
        public IActionResult Detail()
        {
            var product = this.Context.SelectProductById(14);
            ViewData["Title"] = product.Name;
            return View();
        }
    }
}
