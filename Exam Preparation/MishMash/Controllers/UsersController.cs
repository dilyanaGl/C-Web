using System;
using System.Collections.Generic;
using System.Text;
using SIS.MvcFramework;
using SIS.HTTP.Responses;
using SIS.HTTP.Cookies;


namespace Chushka.Controllers
{
    using Models;
    using Services;

    public class UsersController : BaseController
    {
        protected readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IHttpResponse Register() => this.View();     


        [HttpPost]
        public IHttpResponse Register(RegisterViewModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Username) || model.Username.Trim().Length < 4)
            {
                return this.BadRequestErrorWithView("Please provide valid username with length of 4 or more characters.");
            }

            if (string.IsNullOrWhiteSpace(model.Email) || model.Email.Trim().Length < 4)
            {
                return this.BadRequestErrorWithView("Please provide valid email with length of 4 or more characters.");
            }        

            if (string.IsNullOrWhiteSpace(model.Password) || model.Password.Length < 3)
            {
                return this.BadRequestErrorWithView("Please provide password of length 6 or more.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                return this.BadRequestErrorWithView("Passwords do not match.");
            }
            var success = this.userService.RegisterUser(model);      
            
            if(!success)
            {
                return this.BadRequestErrorWithView("User with that username already exists.");
            }
            //var user = this.userService.GetUserInfo(model.Username);
            //var cookie = new HttpCookie("auth", this.UserCookieService.GetUserCookie(user), 7)
            //{
            //    HttpOnly = true
            //};
            //this.Response.AddCookie(cookie);


            return this.Redirect("/Users/Login");
        }

        [HttpGet]
        public IHttpResponse Login() => this.View();

        [HttpPost]
        public IHttpResponse Login(LoginViewModel model)
        {
            if(!this.userService.UserExists(model))
            {
                return this.BadRequestErrorWithView("Invalid credentials.");
            }

            var user = this.userService.GetUserInfo(model.Username);
            
            var cookie = new HttpCookie("auth", this.UserCookieService.GetUserCookie(user), 7)
            {
                HttpOnly = true
            }; 
            this.Response.AddCookie(cookie);
            return this.Redirect("/");
        }

        [HttpGet]
        public IHttpResponse Logout()
        {
            if(!this.Request.Cookies.ContainsCookie("auth"))
            {
                return this.Redirect("/");
            }
            var cookie = this.Request.Cookies.GetCookie("auth");
            cookie.Delete();
            this.Response.AddCookie(cookie);
            return this.Redirect("/");
        }
    }
}
