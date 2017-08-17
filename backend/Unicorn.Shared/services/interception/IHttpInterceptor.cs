using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Unicorn.Shared.services.interception
{
    public interface IHttpInterceptor
    {
        void OnRequest(HttpRequestMessage request);

        void OnResponse(HttpRequestMessage request, HttpResponseMessage response);
    }
}
