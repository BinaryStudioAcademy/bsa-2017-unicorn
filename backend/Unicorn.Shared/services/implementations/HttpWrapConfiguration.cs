using System.Net.Http;
using Unicorn.Shared.services.interfaces;

namespace Unicorn.Shared.services.implementations
{
    class HttpWrapConfiguration : IHttpWrapConfiguration
    {
        public string BasePath => "http://localhost:52309/";

        public ISerializer Serializer => new JsonSerializer();

        public HttpClient GetHttpClient() => new HttpClient();
    }
}
