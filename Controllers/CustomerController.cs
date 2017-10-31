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
    public class CustomerController : Controller
    {
        private WebshopContext Context;
        public CustomerController(WebshopContext context){this.Context = context;}

        [HttpGet("[action]")]
        public IActionResult Index()
        {
            return View(this.Context.SelectAllCustomers());
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Detail(int id)
        {
            if(Request.Cookies["user"] != null) {
                ViewData["user"] = Request.Cookies["user"];
                ViewData["username"] = Request.Cookies["username"];
                }
            return View(this.Context.SelectCustomerById(id));
        }

        [HttpGet("[action]")]
        public IActionResult Create(){return View();}

        [HttpPost("[action]")]
        public IActionResult Create([Bind("Name, Surname, Gender, Email, Password, Street, PostalCode, City")] Customer customer)
        {
            this.Context.Customers.Add(customer);
            this.Context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Edit(int id){return View(this.Context.SelectCustomerById(id));}

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost([Bind("Id ,Name, Surname, Gender, Email, Street, PostalCode, City")] Customer customer)
        {
            var customer2update = this.Context.SelectCustomerById(customer.Id);
            
            customer2update.Name = customer.Name;
            customer2update.Surname = customer.Surname;
            customer2update.Gender = customer.Gender;
            customer2update.Email = customer.Email;
            customer2update.Street = customer.Street;
            customer2update.PostalCode = customer.PostalCode;
            customer2update.City = customer.City;

            this.Context.SaveChanges();
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Delete(int id){return View(this.Context.SelectCustomerById(id));}

        [HttpPost("[action]/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReal(int id)
        {
            this.Context.Customers.Remove(this.Context.SelectCustomerById(id));
            this.Context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}