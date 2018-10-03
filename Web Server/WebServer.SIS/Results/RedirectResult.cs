using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Results
{
    using Http.Headers;
    using Http.Responses;
    using Http.Enums;

    public class RedirectResult : HttpResponse
    {
        public RedirectResult(string location, HttpResponseCode statusCode)
            : base(statusCode)
        {
            this.Headers.Add(new HttpHeader("Location", location));
        }
    }
}
