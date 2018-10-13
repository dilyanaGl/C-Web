using System;
using System.Collections.Generic;
using System.Text;

namespace App.Controllers
{
    using SIS.HTTP.Responses;
    using SIS.HTTP.Requests;
    using SIS.WebServer.Results;

    public class HomeController : BaseController
    {
        public HomeController() 
        {

        }
        public IHttpResponse Index(IHttpRequest request)
        {

            this.ViewData["username"] = this.GetUsername(request);
            this.SetAuthenticationDisplay(request);

            return this.View("Index");
        }

    }
}
