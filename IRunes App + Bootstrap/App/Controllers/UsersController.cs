using System;
using System.Collections.Generic;
using System.Text;

namespace App.Controllers
{
    using SIS.HTTP.Requests;
    using SIS.HTTP.Responses;
    using Services;
    using SIS.HTTP.Cookies;
    using SIS.WebServer.Results;

    public class UsersController : BaseController
    {
        private readonly UserService userService;
        private readonly UserCookieService userCookieService;

        public UsersController()
        {
            this.userCookieService = new UserCookieService();
            this.userService = new UserService();
        }

        public IHttpResponse Register(IHttpRequest request) => this.View("Register");

        public IHttpResponse DoRegister(IHttpRequest request)
        {
            string username = request.FormData["Username"].ToString();
            string password = request.FormData["Password"].ToString();
            string confirmPassword = request.FormData["ConfirmPassword"].ToString();
            string email = request.FormData["Email"].ToString();

            if(!password.Equals(confirmPassword))
            {
                this.SetError("Password and confirm password must match!");
                return this.View("Register");
            }

            this.userService.RegisterUser(username, password, email);
            

            var cookieContent = this.userCookieService.GetUserCookie(username);
            var cookie = new HttpCookie("IRunes_auth", cookieContent, 7) { HttpOnly = true };

            var response = new RedirectResult("/home/index");

            response.Cookies.Add(cookie);

            this.LoginUser(username, request);

            return response;


        }

        public IHttpResponse Login(IHttpRequest request) => this.View("Login");

        public IHttpResponse DoLogin(IHttpRequest request)
        {
            string username = request.FormData["Username"].ToString();
            string password = request.FormData["Password"].ToString();
            if (!this.userService.UserExists(username, password))
            {
                this.SetError("Invalid credentials!");
                return this.View("Login");
            }

            this.LoginUser(username, request);
            var response = /*new HomeController().Index(request);*/ new RedirectResult("/home/index");
            response.Cookies.Add(new HttpCookie("IRunes_auth", this.userCookieService.GetUserCookie(username)));
            return response;


        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            if(!request.Cookies.ContainsCookie("IRunes_auth"))
            {
                return new RedirectResult("/");
            }

            var cookie = request.Cookies.GetCookie("IRunes_auth");
            cookie.Delete();

            var response = new RedirectResult("/");
            response.AddCookie(cookie);

            return response;
        }
    }
}
