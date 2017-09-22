namespace Webshop.Controllers
{
     using System;
     using System.Collections.Generic;
     using System.Linq;

     using Microsoft.AspNetCore.Mvc;

     using Webshop.Models;
     using Webshop.Models.DbXtensions;
     using Webshop.Paginator;
     using Webshop.Xtensions;
     using Webshop.Xtratypes;

    //This is URL for the home page, there are two ways to get at the homepage:
    //-1. is to goto http//:www.example.com/ and 2 to go to http//:www.example.come/Home
    [Route("/")] [Route("/Home")] 
    public class WebShopController : Controller
    {
        private WebshopContext _context;
        public WebShopController(WebshopContext context){
            this._context = context;
        }
        // GET This method is being called when you go to the home page, it displays all products from the database.
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var query = _context.SelectAllProducts();
            var product = query.GetPage(0,4,p => p.Id);
            if(product == null)return null;
            return Ok(product);
        }

        // GET http//:www.example.come/Home/0/16
        //This method selects one product by its ID, this method can be called in example, when a customer
        //clicks a prodcuct to see more details
        [HttpGet("{page_index}/{id}")]
        public IActionResult GetProductByID(int page_index, int page_size, int id)
        {
            //return "value";
            var query = from p in this._context.Products
                        where p.Id == id
                        select p;
            //var product = _context.Products.GetPage<Product>(page_index, page_size,a => a.Id);
            var product = query.GetPage(page_index, 3, t => t.Id);
            if(product == null)return NotFound(product);
            return Ok(product);
        }

        // POST 
        [HttpPost]
        public void Post([FromBody]Product value)
        {
        
        }

        // PUT 
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Product value)
        {
        }

        // DELETE 
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
          
        }
    }
}
