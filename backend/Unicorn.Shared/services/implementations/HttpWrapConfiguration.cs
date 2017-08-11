using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Core.services.interfaces;

namespace Unicorn.Core.services.implementations
{
    class HttpWrapConfiguration : IHttpWrapConfiguration
    {
        public string BasePath => "http://localhost:52309/";

        public ISerializer Serializer => new JsonSerializer();

        public HttpClient GetHttpClient()
        {
            return new HttpClient();
        }

    }
}
