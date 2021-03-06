﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Headers
{
   public class HttpHeaderCollection : IHttpHeaderCollection
    {
        private readonly Dictionary<string, HttpHeader> headers;

        public HttpHeaderCollection()
        {
            this.headers = new Dictionary<string, HttpHeader>();

        }

        public void Add(HttpHeader header)
        {
            this.headers.Add(header.Key, header);

        }

        public bool ContainsHeader(string key)
        {

           return this.headers.ContainsKey(key);
        }

        public HttpHeader GetHeader(string key)
        {

            return this.headers[key];
        }


        public override string ToString()
        {
            return String.Join(Environment.NewLine, this.headers.Values.ToString());

        }
    }
}
