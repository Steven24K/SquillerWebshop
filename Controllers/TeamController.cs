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

    [Route("api/[controller]")]
    public class Teamcontroller : Controller
    {
        private WebshopContext Context;
        public Teamcontroller(WebshopContext context) 
        {
            this.Context = context;
        }
        
        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Billy()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Quinten()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Steven()
        {
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Roos()
        {
            return View();
        }
    }
}
