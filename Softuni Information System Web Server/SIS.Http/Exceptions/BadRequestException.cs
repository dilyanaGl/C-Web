using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Exceptions
{
    public class BadRequestException : Exception
    {
        private const string ExceptionMessage = "The Request was malformed or contains unsupported elements.";

        public BadRequestException()
        {
            throw new InvalidOperationException(ExceptionMessage);

        }

        public static BadRequestException ThrowBadRequestException()
        {
            return new BadRequestException();
        }
    }
}
