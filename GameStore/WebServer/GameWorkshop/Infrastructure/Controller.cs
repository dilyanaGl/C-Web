using System.Collections.Generic;
using System.IO;
using System.Linq;
using HTTPServer.GameWorkshop.Services;
using HTTPServer.GameWorkshop.Services.Contracts;
using HTTPServer.GameWorkshop.Views;
using HTTPServer.Server.Enums;
using HTTPServer.Server.Http;
using HTTPServer.Server.Http.Contracts;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.GameWorkshop.Infrastructure
{
    public abstract class Controller
    {
        public const string DefaultPath = @"C:\Users\User\Downloads\05. CSharp-Web-Development-Basics-Databases-EF-Core-Skeleton\WebServer\GameWorkshop\Resources\{0}.html";
        public const string ContentPlaceholder = "{{{content}}}";

        private IHttpRequest request;
        private readonly IUserService userService;

        protected Controller(IHttpRequest request)
        {
            this.ViewData = new Dictionary<string, string>()
            {
                {"showError", "none"} 
            };
            this.Request = request;
            this.userService = new UserService();
            this.Authentication = new Authentication(false, false);
            SetAuthenticationDisplay();
        }

        protected IDictionary<string, string> ViewData { get; private set; }

        public IHttpRequest Request
        {
            get { return request; }
            private set { request = value; }
        }

        protected IHttpResponse FileViewResponse(string fileName)
        {
            var result = this.ProcessFileHtml(fileName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    result = result.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        protected IHttpResponse RedirectResponse(string route)
            => new RedirectResponse(route);

        protected void AddError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["error"] = errorMessage;
        }

        private string ProcessFileHtml(string fileName)
        {
            var layoutHtml = File.ReadAllText(string.Format(DefaultPath, "layout"));

            var fileHtml = File
                .ReadAllText(string.Format(DefaultPath, fileName));

            var result = layoutHtml.Replace(ContentPlaceholder, fileHtml);

            return result;
        }

        protected bool IsAdmin { get; private set; }

        protected Authentication Authentication { get; set; }

        protected void SetAuthenticationDisplay()
        {
            var authDisplay = "none";
            var adminDisplay = "none";
            var anonymousDisplay = "flex";

            var isLoggedIn = this.Request.Session.Contains(SessionStore.CurrentUserKey);

           if (isLoggedIn)
           {
               authDisplay = "flex";
               anonymousDisplay = "none";

               var email = this.Request.Session.Get<string>(SessionStore.CurrentUserKey);
               this.IsAdmin = this.userService.IsAdmin(email);
               if (IsAdmin)
               {
                   adminDisplay = "flex";
               }

               this.Authentication = new Authentication(true, IsAdmin);
           }

            this.ViewData["authDisplay"] = authDisplay;
            this.ViewData["adminDisplay"] = adminDisplay;
            this.ViewData["anonymousDisplay"] = anonymousDisplay;
        }
    }
}
