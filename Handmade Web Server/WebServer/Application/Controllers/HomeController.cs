using System;
using System.IO;
using HTTPServer.Application.Helpers;
using HTTPServer.Application.Views;
using HTTPServer.Security;
using HTTPServer.Server.Enums;
using HTTPServer.Server.Http.Contracts;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.Application.Controllers
{
    internal class HomeController : ViewReader
    {

        public IHttpResponse Index()
        {
            // var indexHtml = File.ReadAllText(@"Resources\index.html");

            if (!Session.IsUserLoggedIn())
            {
                return this.GetResponse("logIn");
            }

            return GetResponse("index");
        }

     
        public IHttpResponse AboutUs()
        {
            if (!Session.IsUserLoggedIn())
            {
                return this.GetResponse("logIn");
            }

            return GetResponse("aboutUs");
        }

        public IHttpResponse Login()
        {

          return GetResponse("logIn");
        }

        public IHttpResponse Home(string username, string password)
        {
            this.LogInUser(username, password);

            return GetResponse("index");
        }


    }
}