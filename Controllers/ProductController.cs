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
    using Webshop.Models.EntityInfo;
    using Webshop.Utils.Xtratypes;
    using Webshop.Utils.Xtensions;

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private WebshopContext Context; 
        public ProductController(WebshopContext context){
            this.Context = context;
        }

        [HttpGet("[action]")]
        public IActionResult Index(string keyword = null)
        {
            if(keyword == null)return View(this.Context.SelectAllProducts());
            return View(this.Context.SearchProducts(keyword));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Detail(int id)
        {
            return View(this.Context.SelectProductById(id));
        }

        [HttpGet("[action]")]
        public IActionResult Create(){return View();}

        
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(
            "Name, Description, Category, Brand, Price, Gender, Extra, Amount")] 
         ProductInfo product )
        {
              try
              {
                  this.Context.AddProduct(product.Name, product.Description,product.Category, product.Brand,
                  product.Price, product.Gender, product.Extra,product.Amount);
                  return RedirectToAction(nameof(Index));
              }catch
              {
                 return RedirectToAction(nameof(Index));
              }
        }
    }
}
