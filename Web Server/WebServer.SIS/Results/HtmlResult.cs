using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Results
{
    using Http.Responses;
    using Http.Enums;
    using Http.Headers;

   public class HtmlResult : HttpResponse
    {
        public HtmlResult(string content, HttpResponseCode statusCode) 
            : base(statusCode)
        {
            this.Headers.Add(new HttpHeader("Content-Type", "text/html"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }

    }
}
