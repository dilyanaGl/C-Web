using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.Http.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        private const string ExceptionMessage = "The Server has encountered an error.";

        public InternalServerErrorException()
        {
            throw new InvalidOperationException(ExceptionMessage);

        }

        public static InternalServerErrorException ThrowInternalServerErrorException()
        {
            return new InternalServerErrorException();
        }

    }
}
