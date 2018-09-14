using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Attributes;
using SimpleMvcFramework.Contracts;
using SimpleMvcFramework.Controllers;

namespace SimpleMvc.App.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            return this.View();
        }
    }
}
