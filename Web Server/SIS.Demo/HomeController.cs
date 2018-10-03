using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Demo
{
    using Http.Enums;
    using Http.Responses;
    using WebServer.Results;

    public class HomeController
    {
        public IHttpResponse Index()
        {
            string content = "<h1>Hello, World!</h1>";

            return new HtmlResult(content, HttpResponseCode.Ok);

        }
    }
}
