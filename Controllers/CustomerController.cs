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
        //TODO: In most cases the customer id is send from the url, so I have to check everywhere if the GET id corresponds to the Cookie id
        //it is better and safer to store the userId inside a cookie!
        private WebshopContext Context;
        public CustomerController(WebshopContext context){this.Context = context;}

        [HttpGet("[action]")]
        public IActionResult Index(int page = 0, string order = "TIME",string keyword = null)
        {
            if(Request.Cookies["admin"] != null){
                ViewData["keyword"] = keyword ?? "";
                return View(this.Context.SelectAllCustomers(keyword, order).GetPage(page, 50));
                }
            return RedirectToAction("Error403","Error");
        }

        [HttpGet("[action]")]
        public IActionResult Detail(int id = 0)
        {
             if(Request.Cookies["admin"] != null){
                 ViewData["orders"] = this.Context.SelectAllOrders(OrderStatus.ALL ,id);
                 return View(this.Context.SelectCustomerById(id));
             }

            if(Request.Cookies["user"] != null ) {
                int C_ID = Convert.ToInt32(Request.Cookies["user"]);
                ViewData["orders"] = this.Context.SelectAllOrders(OrderStatus.ALL, C_ID);
                return View(this.Context.SelectCustomerById(C_ID));
                }
            return RedirectToAction("Error403","Error");
        }

        [HttpGet("[action]")]
        public IActionResult Create(){
            if(Request.Cookies["admin"] != null){ ViewData["admin"] = Request.Cookies["admin"];}

            if(Request.Cookies["user"] != null) {
                return RedirectToAction("Index", "Home");
                }
            return View();
            }

        [HttpPost("[action]")]
        public IActionResult Create([Bind("Name, Surname, Gender, Email, Password, Street, PostalCode, City, AcceptTerms")] RegisterCustomer customer)
        {
            this.Context.Customers.Add(new Customer{
                Name = customer.Name,
                Surname = customer.Surname,
                Gender = customer.Gender,
                Email = customer.Email,
                Password = customer.Password,
                Street = customer.Street,
                PostalCode = customer.PostalCode,
                City = customer.City
            });
            this.Context.SaveChanges();

            if(Request.Cookies["admin"] != null){
            //When a user creates an account he will be redirected to the home page, when an admin creates a new user account he will be 
            //redirected to the customer table main page.
            return RedirectToAction(nameof(Index));
            }

            //TODO: add feature that the customer will be imedately logged in into the System
            //...
            //...

            return RedirectToAction("Index","Home");

        }

        [HttpGet("[action]")]
        public IActionResult Edit(int id = 0){
            if(Request.Cookies["admin"] != null){ 
                return View(this.Context.SelectCustomerById(id));
                }

            if(Request.Cookies["user"] != null) {
                return View(this.Context.SelectCustomerById(Convert.ToInt32(Request.Cookies["user"])));
                }            
              
              return RedirectToAction("Error403", "Error");
            }

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
            
            if(Request.Cookies["user"] != null){
                 return RedirectToAction(nameof(Detail), new {id = customer.Id});
            }
            //When a user creates an account he will be redirected to the home page, when an admin creates a new user account he will be 
            //redirected to the customer table main page.
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("[action]")]
        public IActionResult Delete(int id = 0){
            
            if(Request.Cookies["admin"] != null){ 
                return View(this.Context.SelectCustomerById(id));
                }

            if(Request.Cookies["user"] != null) {
                return View(this.Context.SelectCustomerById(Convert.ToInt32(Request.Cookies["user"])));
                }

            return RedirectToAction("Error403", "Error");
            }

        [HttpPost("[action]")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReal(int id = 0)
        {
            if(Request.Cookies["admin"] != null){
                this.Context.Customers.Remove(this.Context.SelectCustomerById(id));
            }else{
            this.Context.Customers.Remove(this.Context.SelectCustomerById(Convert.ToInt32(Request.Cookies["user"])));
            }
            this.Context.SaveChanges();

            if(Request.Cookies["user"] != null) {
                //When a user deletes his account also all the cookies will be removed to make sure the user does not still have acces to SelectCustomerById
                //of the website features
                Response.Cookies.Delete("user");
                Response.Cookies.Delete("username");
                return RedirectToAction("Index","Home");
                }
            //When a user creates an account he will be redirected to the home page, when an admin creates a new user account he will be 
            //redirected to the customer table main page.
            return RedirectToAction(nameof(Index));
        }
    }
}