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
            if(keyword == null)return View(this.Context.BindSelectAllProducts());
            return View(this.Context.SearchProducts(keyword));
        }

        [HttpGet("[action]")]
        public IActionResult ProductsTable()
        {
            return View(this.Context.BindSelectAllProducts());
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Detail(int id)
        {
            return View(this.Context.BindSelectProductById(id));
        }

        [HttpGet("[action]")]
        public IActionResult Create(){return View();}

        
        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind(
            "Name, Description, Category, Brand, Price, Gender, Extra, Amount, File")] 
         ProductInfo product)
        {
              try
              {
                this.Context.AddProduct(product.Name, product.Description,product.Category, product.Brand,
                  product.Price, product.Gender, product.Extra,product.Amount);
 
                  return RedirectToAction(nameof(ProductsTable));
              }catch
              {
                 return RedirectToAction(nameof(ProductsTable));
              }
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Delete(int id)
        {
            return View(this.Context.SelectProductById(id));
        }

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReal(int id)
        {
            this.Context.Products.Remove(this.Context.SelectProductById(id));
            this.Context.SaveChanges();
            return RedirectToAction(nameof(ProductsTable));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Edit(int id){return View(this.Context.BindSelectProductById(id));}

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost([Bind(
            "Id,Name, Description, Category, Brand, Price, Gender, Extra, Amount, File")] 
         ProductInfo product)
        {
            Context.Products.Update(this.Context.SelectProductById(product.Id));
            
            Context.SaveChanges();

            return RedirectToAction(nameof(ProductsTable));
        }
    }
}
