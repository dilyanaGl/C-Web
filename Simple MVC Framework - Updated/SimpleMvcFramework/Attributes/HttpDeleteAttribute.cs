using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Attributes
{
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        public override bool IsValid(string requestMethod)
        {
            return requestMethod.ToLower() == "delete";
        }
    }
}
