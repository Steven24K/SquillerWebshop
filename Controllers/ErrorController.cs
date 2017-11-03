namespace Webshop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;


    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [HttpGet("[action]")]
        public IActionResult Error404()
        {
            //Check if admin is logged in 
            if(Request.Cookies["admin"] != null){ ViewData["admin"] = Request.Cookies["admin"];}
               
            //Check if user is looged in
            if(Request.Cookies["user"] != null) {
                ViewData["user"] = Request.Cookies["user"];
                ViewData["username"] = Request.Cookies["username"];
                }
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Error403()
        {
            //Check if admin is logged in 
            if(Request.Cookies["admin"] != null){ ViewData["admin"] = Request.Cookies["admin"];}
               
            //Check if user is looged in
            if(Request.Cookies["user"] != null) {
                ViewData["user"] = Request.Cookies["user"];
                ViewData["username"] = Request.Cookies["username"];
                }
            return View();
        }

    }
}
