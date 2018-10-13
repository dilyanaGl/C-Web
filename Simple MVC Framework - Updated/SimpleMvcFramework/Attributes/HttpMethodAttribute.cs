using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleMvcFramework.Attributes
{
    public abstract class HttpMethodAttribute : Attribute
    {
        public abstract bool IsValid(string requestMethod);
    }
}
