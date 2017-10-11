﻿namespace Webshop.Controllers
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
            ViewData["Product"] = this.Context.SelectAllProducts().ToList();
      
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

        
         [HttpGet("/Detail/{id}")]
        public IActionResult Detail(int id)
        {
            var product = this.Context.SelectProductById(id);
            ViewData["Title"] = product.Name;
            ViewData["Name"] = product.Name;
            ViewData["Price"] = product.Price;
            ViewData["Description"] = product.Description;
            ViewData["Id"] = product.Id;
            return View();
        }

        [HttpGet("/Error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
