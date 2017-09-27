using System.Net;
using System.Net.Http;
using Unicorn.Shared.services.interfaces;

namespace Unicorn.Shared.services
{
    public class HttpWrapResponse : IHttpWrapResponse
    {
        public HttpWrapResponse(HttpResponseMessage message)
        {
            StatusCode = message.StatusCode;
            Body = message.Content.ReadAsStringAsync().Result;
            Raw = message;
        }

        public HttpWrapResponse(HttpStatusCode statusCode, string body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public HttpStatusCode StatusCode { get; protected set; }
        public string Body { get; protected set; }

        public virtual bool Success => StatusCode == HttpStatusCode.OK;

        public HttpResponseMessage Raw { get; protected set; }
    }

    public class HttpWrapResponse<T> : HttpWrapResponse, IHttpWrapResponse<T>
    {
        public HttpWrapResponse(HttpStatusCode statusCode, string body)
            : base(statusCode, body)
        {
        }

        public HttpWrapResponse(HttpResponseMessage message)
            : base(message)
        {
        }

        public T Data { get; set; }
    }
}
