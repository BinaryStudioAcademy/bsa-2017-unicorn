using System.Net.Http;

namespace Unicorn.Shared.services.interception
{
    public interface IHttpInterceptor
    {
        void OnRequest(HttpRequestMessage request);

        void OnResponse(HttpRequestMessage request, HttpResponseMessage response);
    }
}
