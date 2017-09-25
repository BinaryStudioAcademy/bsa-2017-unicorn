using System.Net.Http;

namespace Unicorn.Shared.services.interfaces
{
    public interface IHttpWrapConfiguration
    {
        string BasePath { get; }
        ISerializer Serializer { get; }
        HttpClient GetHttpClient();
    }
}
