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
    using Webshop.Models.ViewModels;

    [Route("/")] [Route("/[controller]")]
    public class HomeController : Controller
    {
        private WebshopContext Context; 
        private CookieOptions Options;
        public HomeController(WebshopContext context){
            this.Context = context;
            this.Options = new CookieOptions{Expires = DateTime.Now.AddYears(10)};

            
        }
        [HttpGet]
        public IActionResult Index()
        {
            if(Request.Cookies["comeBack"] == null){
                Response.Cookies.Append("comeBack","I was here!", Options);
            }
 
            if(this.Context.Products.Count() == 0){
            string[] sample1 = new string[] {"rolex", "jeans" , "something" , "nothing" , "gucci" , "polo" , "expesive"};
            string[] sample2 = new string[] {"Video", "shirt" , "belt", "product" , "to buy", "cars jeans" ,"cap"};
            Random rnd = new Random();

            for(int i=0;i<30;i++){
            //To add a product to the database
            Product product = new Product{
                  Name = sample1[rnd.Next(0, sample1.Length-1)] + " " + sample2[rnd.Next(0, sample2.Length-1)],
                  Description = "Lorum impsum...",
                  Category = "Sample product",
                  Brand = "Sample brand",
                  Price = rnd.Next(50, 10000),
                  Gender = Gender.UNSPECIFIED,
                  Extra = Extra.REGULAR,
                  Amount = rnd.Next(0, 50)
            };
            
            
            this.Context.Add(product);
            }

            this.Context.SaveChanges();
            }

            return View(this.Context.SelectAllProducts().GetPage(0,3,p=>p.DateAdded).Items.ToList());
        }

        [HttpGet("[action]")]
        public IActionResult About()
        {
            return View();
        }
  
        [HttpGet("[action]")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("[action]")]
        public IActionResult Contact([FromForm]ContactViewModel data){
            System.Console.WriteLine("Contact form! " + data + " " + data.Name);

            //TODO: Send email

            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Team()
        {
            return RedirectToAction("Index","Team");            
        }

        [HttpGet("[action]")]
        public IActionResult Terms()
        {
            return View();           
        }

        [HttpGet("[action]")]
        public IActionResult Video()
        {
            return View();           
        }

        [HttpGet("[action]")]
        public IActionResult FAQ()
        {
            return RedirectToAction("Index","Faq");           
        }
    }
}
