using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SIS.Http.Requests
{
    using Headers;
    using SIS.Http.Enums;
    using Exceptions;
    using Common;
    using SIS.Http.Cookies;
    using SIS.Http.Sessions;

    public class HttpRequest : IHttpRequest
    {

        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, object>();
            this.QueryData = new Dictionary<string, object>();
            this.Headers = new HttpHeaderCollection();
            this.Cookies = new HttpCookieCollection();
            ParseRequest(requestString);

        }

        public string Path { get; private set; }
        public string Url { get; private set; }
        public Dictionary<string, object> FormData { get; }
        public Dictionary<string, object> QueryData { get; }
        public IHttpHeaderCollection Headers { get; }
        public HttpRequestMethod RequestMethod { get; private set; }

        public IHttpCookieCollection Cookies { get; set; }

        public IHttpSession Session { get; set; }

        private void ParseRequest(string requestString)
        {
            var requestLines = requestString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            ParseFirstLine(requestLines[0].Trim());
            ParseHeaders(requestString.Split(new[] { Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray());
            this.ParseCookies();

            if (this.RequestMethod == HttpRequestMethod.Post)
            {
                ParseQueryData(requestString);
            }
        }

        private void ParseCookies()
        {
            if (this.Headers.ContainsHeader("Set-Cookie"))
            {
                var cookieHeader = this.Headers.GetHeader("Set-Cookie");

                var cookieHeaderValue = cookieHeader.Value;
                var cookiePairs = cookieHeaderValue.Split(new[] { "; "}, StringSplitOptions.RemoveEmptyEntries);
               foreach(var pair in cookiePairs)
                {
                    var keyValue = pair.Trim().Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
                    var cookie = new HttpCookie(keyValue[0].Trim(), keyValue[1].Trim());
                    this.Cookies.Add(cookie);
                }              
            }
        }

        private void ParseFirstLine(string firstLine)
        {

            var firstLinesParts = firstLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            HttpRequestMethod requestMethod = HttpRequestMethod.Get;

            if (!this.IsValidRequestLine(firstLinesParts)
                || !Enum.TryParse<HttpRequestMethod>(firstLinesParts[0], true, out requestMethod))
            { 
                BadRequestException.ThrowBadRequestException();

            }
            this.RequestMethod = requestMethod;
            this.Url = firstLinesParts[1];
            var urlParts = this.Url.Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries);
            this.Path = urlParts[0];

            if (urlParts.Length > 1)
            {
                var keyValuePairs = urlParts[1].Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var pair in keyValuePairs)
                {
                    var pairParts = pair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                    if (pairParts.Length != 2)
                    {
                        BadRequestException.ThrowBadRequestException();

                    }

                    this.QueryData.Add(pairParts[0], pairParts[1]);

                }

            }


        }
        private bool IsValidRequestLine(string[] firstLine)
        {
            return firstLine.Length == 3
                && firstLine[2].Equals(Common.GlobalConstants.HttpOneprotocolFragment, 
                StringComparison.InvariantCultureIgnoreCase);

        }

        private void ParseHeaders(string[] requestLines)
        {            

            foreach (var line in requestLines)
            {
                var keyValuePair = line.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

           
                if (keyValuePair.Length != 2 
                    && !(keyValuePair[0].Trim().Equals(GlobalConstants.HostHeaderKey, StringComparison.InvariantCultureIgnoreCase)))
                {
                    BadRequestException.ThrowBadRequestException();
                }

                var key = keyValuePair[0].Trim();
                var value = keyValuePair[1].Trim();

                if(key.Equals(GlobalConstants.HostHeaderKey, StringComparison.InvariantCultureIgnoreCase))
                {
                    value = String.Join("", keyValuePair.Skip(1)).Trim();
                }

                this.Headers.Add(new HttpHeader(key, value));

            }

        }


        private void ParseQueryData(string requestString)
        {

            var requestParts = requestString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            if (requestParts.Length != 2)
            {


                BadRequestException.ThrowBadRequestException();

            }

            var formDataPairs = requestParts[1].Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var pair in formDataPairs)
            {
                var pairParts = pair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                this.FormData.Add(pairParts[0], pairParts[1]);

            }

        }

        


    }
}
