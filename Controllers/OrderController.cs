namespace Webshop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Webshop.Models;
    using Webshop.Models.EntityInfo;
    using Webshop.Models.DbXtensions;
    using Webshop.Utils.Xtratypes;
    using Webshop.Utils.Xtensions;

    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private WebshopContext Context; 
        public OrderController(WebshopContext context){this.Context = context;}

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            if(this.HttpContext.Request.Cookies["admin"] != null)return View();
            return RedirectToAction("Error404", "Error");
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View(this.Context.SelectOrderDetails(Convert.ToInt32(HttpContext.Request.Cookies["user"])));
        }

        [HttpGet("[action]")]
        public IActionResult Edit()
        {
            throw new NotImplementedException("Steven lost dit wel op!!!!");
        }

        [HttpGet("[action]")]
        public IActionResult Cancel()
        {
            throw new NotImplementedException("Steven lost dit wel op!!!!");
        }
    }
}