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
    using Webshop.Utils.ImageProvider;

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private WebshopContext Context; 
        // const string SessionKeyword = "_keyword";
        // const string SessionOrder = "_order";
        public ProductController(WebshopContext context){
            this.Context = context;
        }

        [HttpGet("[action]")]
        public IActionResult Index(string keyword = null, string order = "NAME")
        {
            var p = this.Context.SelectAllProducts(keyword,order);

            List<string> urls = new List<string>();
            foreach(var product in p){
                var html = ImageCollector.GetHtmlCode(product.Name);
                List<string> images = ImageCollector.GetUrls(html);
                urls.Add(images.FirstOrDefault());
                    
            }
            
            ViewData["urls"] = ImageCollector.GetUrls(ImageCollector.GetHtmlCode("hond"));

            if(keyword != null){ 
                ViewData["keyword"] = keyword;
                ViewData["count"] = p.Count();
                }
            
            return View(p);
        }

        [HttpGet("[action]")]
        public IActionResult ProductsTable(string keyword = null, string order = "TIME")
        {
            //TODO: Use sessions in such a way that the order and search term will be remembered.
            // if(keyword != null)HttpContext.Session.SetString(SessionKeyword,keyword);
            // if(order != null) HttpContext.Session.SetString(SessionOrder, order);
            //Check if admin is logged in 
            if(Request.Cookies["admin"] != null){ 
                if(keyword == null)return View(this.Context.SelectAllProducts(keyword,order));
                return View(this.Context.SelectAllProducts(keyword,order));
            }
            return RedirectToAction("Error403","Error");
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Detail(int id)
        {
            Product p = this.Context.SelectProductById(id);
            ViewData["Id"] = p.Id;
            ViewData["Name"] = p.Name;
            ViewData["Price"] = p.Price.FormatPrice();
            ViewData["Descr"] = p.Description;
            switch (this.Context.IsinStock(id))
            {
                case StockInicator.OUTOFORDER://Do not change this outcome, this has a high dependency on it its view
                     ViewData["stock"] = "Out of stock";
                     break;
                case StockInicator.LESSTHANFIVE:
                      ViewData["stock"] = "Less than 5 in stock";
                     break;
                case StockInicator.PLENTY:
                      ViewData["stock"] = "High quantity in stock";
                      break;
                default:
                      ViewData["stock"] = "Currently unavailable";
                      break;
            }
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Create(){
            if(Request.Cookies["admin"] != null){ 
            return View();
            }
            return RedirectToAction("Error403","Error");
        }

        
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
        public IActionResult Delete(int id){
            if(Request.Cookies["admin"] != null){ 
            return View(this.Context.SelectProductById(id));
            }
            return RedirectToAction("Error403","Error");
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
        public IActionResult Edit(int id){
            if(Request.Cookies["admin"] != null){ 
            return View(this.Context.SelectProductById(id));
            }
            return RedirectToAction("Error403","Error");
        }

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
            product2update.Brand = product.Brand;
            product2update.Price = product.Price;
            product2update.Gender = product.Gender;
            product2update.Extra = product.Extra;
            product2update.Amount = product.Amount; 
            
            this.Context.SaveChanges();
            
            return RedirectToAction(nameof(ProductsTable));
        }
    }
}
