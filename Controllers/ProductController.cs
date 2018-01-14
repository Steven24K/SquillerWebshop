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
    using Webshop.Utils.Paginator;

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private WebshopContext Context; 
        public ProductController(WebshopContext context){
            this.Context = context;
        }

        [HttpGet("[action]")]
        public IActionResult Index(int page = 0, string keyword = null, string order = "NAME", Gender gender = Gender.ALL, string category = null, Extra extra = Extra.ALL, string brand = "all")
        {
            var p = this.Context.SelectAllProducts(keyword, order, gender, category, extra, brand);

            ViewData["keyword"] = keyword;
            ViewData["gender"] = gender;
            ViewData["category"] = category;
            ViewData["extra"] = extra;
            
            ViewData["brands"] = this.Context.SelectAllBrands();

            ViewData["count"] = p.Count();
            //The '??' is a special syntact in C# that if the result is null, than it has an alternative value
            return View(p.GetPage(page,50) ?? new Page<Product>{Index = 0, TotalPages = 0, Items = new Product[]{/*Empty list of products*/}});
        }

        [HttpGet("[action]")]
        public IActionResult ProductsTable(int page = 0, string keyword = null, string order = "TIME")
        {
            //Check if admin is logged in 
            if(Request.Cookies["admin"] != null){ 
                @ViewData["keyword"] = keyword ?? "";
                return View(this.Context.SelectAllProducts(keyword,order).GetPage(page, 50) ?? new Page<Product>{Index = 0, TotalPages = 1, Items = new Product[]{}});
            }
            return RedirectToAction("Error403","Error");
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Detail(int id)
        {
            Product p = this.Context.SelectProductById(id);
            ViewData["Id"] = p.Id;
            ViewData["Image"] = p.ImageUrl;
            ViewData["Name"] = p.Name;
            ViewData["Price"] = p.Price.FormatPrice();
            ViewData["Descr"] = p.Description;
            ViewData["brand"] = p.Brand;

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

            ViewData["recommended"] = this.Context.SelectRecommendedProducts(p).GetPage(0,4).Items;
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
            "Name, Description, Category, Brand, Price, Gender, Extra, Amount, ImageUrl")] 
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
            "Id,Name, Description, Category, Brand, Price, Gender, Extra, Amount, ImageUrl")] 
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
            product2update.ImageUrl = product.ImageUrl;
            
            this.Context.SaveChanges();
            
            return RedirectToAction(nameof(ProductsTable));
        }
    }
}
