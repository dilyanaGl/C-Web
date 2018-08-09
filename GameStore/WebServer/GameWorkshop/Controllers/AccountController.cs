using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HTTPServer.GameWorkshop.Infrastructure;
using HTTPServer.GameWorkshop.Services;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.GameWorkshop.ViewModels.Account;
using HTTPServer.Server.Http;
using HTTPServer.Server.Http.Contracts;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.GameWorkshop.Controllers
{
    public class AccountController : Controller
    {
        private const string RegisterView = @"\Account\Register";
        private const string LoginView = @"\Account\login";
        private const string IndexView = @"\Home\Home";

        private readonly IUserService userService;

        public AccountController(IHttpRequest request) : base(request)
        {
            this.userService = new UserService();
        }

        public IHttpResponse Login()
        {
            return this.FileViewResponse(LoginView);
        }
        
        public IHttpResponse Login(LoginViewModel model)
        {

            if (!userService.Find(model.Email, model.Password))
            {

                this.AddError("Invalid details!");
                return new RedirectResponse("/account/login");
            }

            this.Request.Session.Add(SessionStore.CurrentUserKey, model.Email);
          
            return new RedirectResponse("/");
        }

        public IHttpResponse Logout()
        {
            this.Request.Session.Clear();
            
            SetAuthenticationDisplay();

            return this.FileViewResponse(LoginView);
        }

        public IHttpResponse Register()
        {
            
            return FileViewResponse(RegisterView);
        }

       public IHttpResponse Register(RegisterViewModel model)
        {
           
            var error = ValidateModel(model);

            if (error != null)
            {
                this.AddError(error);
                return new RedirectResponse("/account/register");
            }
            
            var success = this.userService.Create(model.Email, model.FullName, model.Password);

            if (!success)
            {
                return new RedirectResponse("/account/register");
            }

            this.Request.Session.Add(SessionStore.CurrentUserKey, model.Email);

            return new RedirectResponse("/");
        }

        private string ValidateModel(RegisterViewModel model)
        {
            var context =new ValidationContext(model);
            var validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, context, validationResults, true) == false)
            {
                foreach (var result in validationResults)
                {
                    if (result != ValidationResult.Success)
                    {
                        return result.ErrorMessage;
                    }
                }
            }


            return null;

        }

    }
}
