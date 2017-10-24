namespace Webshop.Controllers
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;

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

        [HttpGet("[action]")]
        public IActionResult ProductsTable()
        {
            return View(this.Context.SelectAllProducts());
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
         Product product)
        {
              try
              {
                  this.Context.Products.Add(product);
                  this.Context.SaveChanges();
                  return RedirectToAction(nameof(ProductsTable));
              }catch
              {
                 return RedirectToAction(nameof(ProductsTable));
              }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Delete(int id){return View(this.Context.SelectProductById(id));}

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReal(int id)
        {
            this.Context.Products.Remove(this.Context.SelectProductById(id));
            this.Context.SaveChanges();
            return RedirectToAction(nameof(ProductsTable));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Edit(int id){return View(this.Context.SelectProductById(id));}

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost([Bind(
            "Id,Name, Description, Category, Brand, Price, Gender, Extra, Amount")] 
         Product product)
        {
            //Changing reccords....
            //...
            //To execute this action the database must be simplified
            var product2update = this.Context.SelectProductById(product.Id);

            product2update.Name = product.Name;
            product2update.Description = product.Description;
            product2update.Category = product.Category;
            product2update.Price = product.Price;
            product2update.Gender = product.Gender;
            product2update.Extra = product.Extra;
            product2update.Amount = product.Amount; 
            
            this.Context.SaveChanges();
            
            return RedirectToAction(nameof(ProductsTable));
        }
    }
}
