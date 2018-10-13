using System;
using System.Collections.Generic;
using System.Text;
using SimpleMvcFramework.Contracts;
using SimpleMvcFramework.Controllers;
using SimpleMvcFramework.Attributes;
using SimpleMvcFramework.ActionResults;
namespace Demo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => this.View();

        [HttpGet]
        public IActionResult Redirect() => new RedirectResult("/");
    }
}
