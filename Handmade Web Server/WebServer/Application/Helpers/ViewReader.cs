using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HTTPServer.Server.Enums;
using HTTPServer.Application.Views;
using HTTPServer.Security;
using HTTPServer.Server.Http.Contracts;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.Application.Helpers
{
    public abstract class ViewReader
    {
        private const string defaultPath = @"Application\Resources\{0}.html";

        private const string placeholder = "{{{content}}}";

        public IHttpResponse GetResponse(string fileName)
        {
            var result = GetFileName(fileName);
            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        public IHttpResponse GetResponse(string fileName, Dictionary<string, string> keyValuePairs)
        {
            var result = GetFileName(fileName);

            if (keyValuePairs != null && keyValuePairs.Any())
            {
                foreach (var keyValuePair in keyValuePairs)
                {
                    result = result.Replace($"{{{{{{{keyValuePair.Key}}}}}}}", keyValuePair.Value);
                }
            }

            return new ViewResponse(HttpStatusCode.Ok, new FileView(result));
        }

        public void LogInUser(string username, string password)
        {
            var user = new User(username, password);

            Session.User = user;
        }

        private string GetFileName(string fileName)
        {
            var layout = File.ReadAllText(string.Format(defaultPath, "layout"));
            var content = File.ReadAllText(string.Format(defaultPath, fileName));
            var result = layout.Replace(placeholder, content);
            return result;
        }
    }
}
