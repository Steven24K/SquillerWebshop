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
            return View();
        }

    }
}
