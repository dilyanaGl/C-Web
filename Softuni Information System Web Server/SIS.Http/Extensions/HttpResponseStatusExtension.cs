using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Extensions
{
    using Enums;

    public static class HttpResponseStatusExtension
    {
        public static string GetResponseLine(this HttpResponseCode statusCode)
        {
            return $"{(int)statusCode} {statusCode.ToString()}";

        }
    }
}
