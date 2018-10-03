using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SIS.Http.Responses
{
    using Headers;
    using Enums;
    using Common;
    using Extensions;
    using Cookies;

    public class HttpResponse : IHttpResponse
    {
        public HttpResponse() { }
        public HttpResponse(HttpResponseCode statusCode)
        {
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            this.Content = new byte[0];
            this.StatusCode = statusCode;
        }


        public HttpResponseCode StatusCode { get; set; }

        public IHttpHeaderCollection Headers { get; private set; }

        public IHttpCookieCollection Cookies { get; set; }

        public byte[] Content { get; set; }

        public void AddHeader(HttpHeader header)
        {
            this.Headers.Add(header);
        }


        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append($"{GlobalConstants.HttpOneprotocolFragment} " +
                $"{this.StatusCode.GetResponseLine()}")
            .Append(Environment.NewLine)
            .AppendLine($"{this.Headers}");


            if (this.Cookies.HasCookies())
            {
                result.Append($"Set-Cookie: {this.Cookies}").Append(Environment.NewLine);
            }

            result.Append(Environment.NewLine);

            return result.ToString();

        }

        public byte[] GetBytes()
        {
            return Encoding.UTF8.GetBytes(this.ToString()).Concat(this.Content).ToArray();

        }

        public void AddCookie(HttpCookie cookie)
        {
            if (cookie != null)
            {
                this.Cookies.Add(cookie);
            }

        }
    }
}
