using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.services.interfaces
{
    public interface IHttpWrapResponse<out T> : IHttpWrapResponse
    {
        T Data { get; }
    }

    public interface IHttpWrapResponse
    {
        HttpStatusCode StatusCode { get; }
        string Body { get; }
        bool Success { get; }
        HttpResponseMessage Raw { get; }
    }
}
