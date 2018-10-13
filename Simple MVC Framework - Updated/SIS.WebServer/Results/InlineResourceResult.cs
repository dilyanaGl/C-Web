using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.WebServer.Results
{
    using HTTP.Headers;
    using HTTP.Enums;
    using HTTP.Responses;

    public class InlineResourceResult : HttpResponse
    {
        public InlineResourceResult(byte[] content, HttpResponseStatusCode responseCode) : base(responseCode)
        {
            this.Headers.Add(new HttpHeader(HttpHeader.ContentLength, content.Length.ToString()));
            this.Headers.Add(new HttpHeader(HttpHeader.ContentDisposition, "inline"));
            this.Content = content;

        }
    }
}
