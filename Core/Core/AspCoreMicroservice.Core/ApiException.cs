using System;
using System.Net;

namespace AspCoreMicroservice.Core
{
    public class ApiException : Exception
    {
        public ProblemDetails Details { get; set; }

        public ApiException(string message)
            : base(message)
        {
            Details = new ProblemDetails
            {
                Status = (int) HttpStatusCode.InternalServerError,
                Detail = message
            };
        }

        public ApiException(int errorCode, string message)
            : base(message)
        {
            Details = new ProblemDetails
            {
                Status = errorCode,
                Detail = message
            };
        }

        public ApiException(string message, ProblemDetails details)
            : base(message)
        {
            Details = details;
        }
    }
}
