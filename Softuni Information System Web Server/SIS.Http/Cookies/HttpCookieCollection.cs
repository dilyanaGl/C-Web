using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SIS.Http.Cookies
{
    public class HttpCookieCollection : IHttpCookieCollection
    {
        public HttpCookieCollection()
        {
            this.cookies = new Dictionary<string, HttpCookie>();

        }
        private Dictionary<string, HttpCookie> cookies;

        public void Add(HttpCookie cookie)
        {

            this.cookies.Add(cookie.Key, cookie);
        }


        public bool ContainsCookie(string key)
        {
            return this.cookies.ContainsKey(key);

        }


       public HttpCookie GetCookie(string key)
        {
            if (!ContainsCookie(key))
            {
                return null;
            }

            return this.cookies[key];
        }

        public bool HasCookies()
        {
            return this.cookies.Any();
        }

        public override string ToString()
        {
            return string.Join("; ", this.cookies.Values);

        }

    }
}
