using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.services
{
    public class HttpWrapException : Exception
    {
        public HttpWrapException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public HttpWrapException(string message)
            : base(message)
        {
        }
    }

    public class HttpWrapHttpException : HttpWrapException
    {
        public HttpWrapHttpException(HttpStatusCode statusCode, string responseBody)
            : base($"Request responded with status code={statusCode}, response={responseBody}")
        {
            StatusCode = statusCode;
            ResponseBody = responseBody;
        }

        public HttpStatusCode StatusCode { get; private set; }
        public string ResponseBody { get; private set; }
    }
}
