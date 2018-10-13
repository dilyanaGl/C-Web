using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.ActionResults
{
    using Contracts;


    public class RedirectResult : IRedirectable
    {
        public RedirectResult(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; private set; }

        public string Invoke() => this.RedirectUrl;
    }
}
