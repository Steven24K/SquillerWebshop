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

    [Route("[controller]")]
    public class ProductController : Controller
    {
        private WebshopContext Context; 
        public ProductController(WebshopContext context){
            this.Context = context;
        }

        [HttpGet("[action]/{id}")]
        public IActionResult ById(int id)
        {
            var product = this.Context.SelectProductById(id);
            ViewData["Title"] = product.Name;
            ViewData["Name"] = product.Name;
            ViewData["Price"] = product.Price;
            ViewData["Description"] = product.Description;
            ViewData["Id"] = product.Id;
            return View();
        }

        [HttpGet("[action]/{index}")]
        public IActionResult Get(int index)
        {
            var product = this.Context.SelectAllProducts().GetPage(index,3,p => p.Id).Items.ToList();
            ViewData["Product"] = product;
            ViewData["Prev"] = index -1;
            ViewData["Next"] = index +1;
            ViewData["Lenght"] = product.Count;
            return View();
        }
       
       [HttpGet("[action]/{keyword}")]
       public IActionResult Search(string keyword)
       {
           ViewData["SearchResults"] = this.Context.SearchProducts(keyword);
           return View();
       }

    }
}
