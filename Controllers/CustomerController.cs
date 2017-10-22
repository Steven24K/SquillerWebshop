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
            return View(this.Context.BindSelectAllCustomer());
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Detail(int id)
        {
            return View(this.Context.BindSelectCustomerById(id));
        }

        [HttpGet("[action]")]
        public IActionResult Create(){return View();}

        [HttpPost("[action]")]
        public IActionResult Create([Bind("Name, Surname, Gender, Email, Password, Street, PostalCode, City")] CustomerInfo customer)
        {
            this.Context.AddCustomer(customer.Name,customer.Surname,customer.Gender,customer.Email,
            customer.Password,customer.Street, customer.PostalCode,customer.City);
            this.Context.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [HttpGet("[action]/{id}")]
        public IActionResult Edit(int id){return View(this.Context.BindSelectCustomerById(id));}

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost()
        {
            //Edit post: To execute this action the databsase must be simplified
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