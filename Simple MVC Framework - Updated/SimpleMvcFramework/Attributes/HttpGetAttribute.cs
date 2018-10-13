using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Attributes
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            return requestMethod.ToLower() == "get";
        }
    }
}
