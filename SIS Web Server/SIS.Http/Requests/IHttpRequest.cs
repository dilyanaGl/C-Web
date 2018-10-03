using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Requests
{
    using Enums;
    using Headers;
    using Cookies;
    using Sessions;

    public interface IHttpRequest
    {
        string Path { get; }
        string Url { get; }
        Dictionary<string, object> FormData { get; }
        Dictionary<string, object> QueryData { get; }
        IHttpHeaderCollection Headers { get; }
        HttpRequestMethod RequestMethod { get; }
        IHttpCookieCollection Cookies { get; }
        IHttpSession Session { get; set; }

    }
}
