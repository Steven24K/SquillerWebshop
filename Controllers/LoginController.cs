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

     [Route("/users")]
    public class LoginController : Controller
    {
        private WebshopContext Context; 
        public LoginController(WebshopContext context){
            this.Context = context;
        }
         
        [HttpGet("/Login")]
        public IActionResult Login()
        {
           return View();
        }

        [Route("/Signup")]
        public IActionResult Signup()
        {
           return View();
        }
    }
}