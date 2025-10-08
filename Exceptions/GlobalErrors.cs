
using System;

namespace AppApi.Exceptions
{
    public class GlobalError : Exception
    {
        public int StatusCode { get; }

        public GlobalError(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}