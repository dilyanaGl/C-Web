using HTTPServer.ByTheCakeApplication.Service;
using HTTPServer.ByTheCakeApplication.Service.User;
using HTTPServer.ByTheCakeApplication.ViewModels;
using HTTPServer.ByTheCakeApplication.ViewModels.Account;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;

namespace HTTPServer.ByTheCakeApplication.Controllers
{
    using System;
    using Infrastructure;
    using Models;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;

    public class AccountController : Controller
    {
        private const string RegisterView = @"account\register";

        private const string LoginView = @"account\login";

        private readonly IUserService userService;

        public AccountController()
        {
            this.userService = new UserService();
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            if (!req.Session.Contains(SessionStore.CurrentUserKey))
            {
                throw new InvalidOperationException("There is no logged in user");
            }

            var username = req.Session.Get<string>(SessionStore.CurrentUserKey);

            var profile = this.userService.Profile(username);

            if (profile == null)
            {
                throw new InvalidOperationException($"User with username {username} could not be found");
            }

            return this.FileViewResponse(@"account\profile");

            return null;
        }

        public IHttpResponse Register()
        {
            SetViewData();

            return this.FileViewResponse(RegisterView);
        }

        public IHttpResponse Register(IHttpRequest req, RegisterViewModel model)
        {
            SetViewData();

            if (model.Username.Length < 3 || model.Password.Length < 3 || model.ConfirmPassword != model.Password)
            {

                ApplyError("Invalid user details");

                return this.FileViewResponse(RegisterView);
            }

            var success = userService.Create(model.Username, model.Password);

            if (!success)
            {

                ApplyError($"User with username {model.Username} already exists");

                return this.FileViewResponse(RegisterView);
            }
            else
            {
                LoginUser(req, model.Username);
                return new RedirectResponse("/");
            }

        }


        public IHttpResponse Login()
        {
            SetViewData();

            return this.FileViewResponse(LoginView);
        }

        public IHttpResponse Login(IHttpRequest req, LoginViewModel model)
        {
            //const string formNameKey = "name";
            //const string formPasswordKey = "password";

            //if (!req.FormData.ContainsKey(formNameKey)
            //    || !req.FormData.ContainsKey(formPasswordKey))
            //{
            //    return new BadRequestResponse();
            //}

            //var name = req.FormData[formNameKey];
            //var password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(model.Username)
                || string.IsNullOrWhiteSpace(model.Password))
            {
                ApplyError("You have empty fields");

                return this.FileViewResponse(LoginView);
            }

            var success = this.userService.Find(model.Username, model.Password);
            if (!success)
            {
                ApplyError("Incorrect username or password!");

                return this.FileViewResponse(LoginView);

            }
            else
            {
                this.LoginUser(req, model.Username);

                return new RedirectResponse("/");
            }

        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }

        private void SetViewData()
        {

            this.ViewData["authDisplay"] = "none";
        }

        private void LoginUser(IHttpRequest req, string name)
        {
            req.Session.Add(SessionStore.CurrentUserKey, name);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());
        }
    }
}
