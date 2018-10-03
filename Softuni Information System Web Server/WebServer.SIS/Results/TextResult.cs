using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Results
{
    using Http.Responses;
    using Http.Enums;
    using Http.Headers;

    public class TextResult : HttpResponse
    {
        public TextResult(string content, HttpResponseCode statusCode)
            : base(statusCode)
        {
            this.Headers.Add(new HttpHeader("Content-Type", "text/plain"));
            this.Content = Encoding.UTF8.GetBytes(content);
        }
    }
}
