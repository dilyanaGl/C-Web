using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace Chushka.Controllers
{
    using Services;
    using Models;
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }


        public IHttpResponse Index()
        {

            if (this.User.Username != null)
            {
                var model = productService.Index();
                return this.View("LoggedInUser", model);
            }

            return this.View();
        }
    }
}
