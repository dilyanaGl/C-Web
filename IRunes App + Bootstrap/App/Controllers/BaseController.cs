using System;
using System.Collections.Generic;
using System.Text;

namespace App.Controllers
{
    using SIS.HTTP.Responses;
    using System.IO;
    using SIS.WebServer.Results;
    using SIS.HTTP.Enums;
    using SIS.HTTP.Requests;
    using System.Linq;
    using SIS.HTTP.Cookies;
    using Services;

    public abstract class BaseController
    {
        private const string DefaultPath = "../../../Views/";
        private const string suffix = ".html";
        private const string content = "{{{content}}}";

        private readonly UserCookieService userCookieService;

        protected BaseController()
        {
            this.userCookieService = new UserCookieService();

            this.ViewData = new Dictionary<string, string>()
            {
                {"showError", "none"},
                { "guest", "flex"},
                { "user", "none"}
            };

           

        }

        protected void SetError(string errorMessage)
        {
            this.ViewData["showError"] = "block";
            this.ViewData["errorMessage"] = errorMessage;
        }



        protected void SetAuthenticationDisplay(IHttpRequest request)
        {
            if (request.Cookies.ContainsCookie("IRunes_auth"))
            {
                this.ViewData["guest"] = "none";
                this.ViewData["user"] = "flex";
            }
        }
     

        public IHttpResponse View(string viewName)
        {
            string content = ProcessFileContent(viewName);

            if (this.ViewData.Any())
            {
                foreach (var value in this.ViewData)
                {
                    content = content.Replace($"{{{{{{{value.Key}}}}}}}", value.Value);
                }

            }

            return new HtmlResult(content, HttpResponseStatusCode.Ok);

        }

        public bool IsAuthenticated(IHttpRequest request) => this.GetUsername(request) != null;

        public string ProcessFileContent(string fileName)
        {
            var layoutPath = string.Concat(DefaultPath, "Layout", suffix);
            string filePath = string.Concat(DefaultPath, fileName, suffix);

            var layoutHtml = File.ReadAllText(layoutPath);


            var fileHtml = File

                .ReadAllText(filePath);

            var result = layoutHtml.Replace(content, fileHtml);

            return result;

        }

       

        public Dictionary<string, string> ViewData { get; protected set; }

        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie("IRunes_auth"))
            {
                return null;
            }

            var cookie = request.Cookies.GetCookie("IRunes_auth");

            var cookieContent = cookie.Value;

            var userName = this.userCookieService.GetUserData(cookieContent);

            return userName;

        }

        protected void LoginUser(string username, IHttpRequest request)
        {
            var cookieContent = this.userCookieService.GetUserCookie(username);


            if (!request.Cookies.ContainsCookie("IRunes_auth"))
            {
                request.Cookies.Add(new HttpCookie("IRunes_auth", cookieContent));
            }
            SetAuthenticationDisplay(request);
           request.Session.AddParameter("username", username);

            

        }



    }
}
