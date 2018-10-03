using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Responses
{
    using Enums;
    using Headers;
    using Cookies;

    public interface IHttpResponse
    {
        HttpResponseCode StatusCode { get; set; }
        IHttpHeaderCollection Headers { get; }
        byte[] Content { get; set; }
        void AddHeader(HttpHeader header);
        byte[] GetBytes();
        IHttpCookieCollection Cookies { get; }
        void AddCookie(HttpCookie cookie);
    }
}
