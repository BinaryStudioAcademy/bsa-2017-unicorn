using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Unicorn.Shared.services.interception;

namespace Unicorn.Shared.services.interfaces
{
    public interface IHttpWrapClient
    {
        Task<IHttpWrapResponse> GetAsync(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        IHttpWrapResponse Get(string path, object payload, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        IHttpWrapResponse Get(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        Task<IHttpWrapResponse> GetAsync(string path, object payload, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        Task<IHttpWrapResponse<T>> GetAsync<T>(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        Task<IHttpWrapResponse<T>> GetAsync<T>(string path, object payload,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        Task<IHttpWrapResponse> PutAsync<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        IHttpWrapResponse Put<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        Task<IHttpWrapResponse> PostAsync<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        IHttpWrapResponse Post<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        Task<IHttpWrapResponse> DeleteAsync(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        IHttpWrapResponse Delete(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        Task<IHttpWrapResponse> PatchAsync<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        IHttpWrapResponse Patch<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null);

        void AddInterceptor(IHttpInterceptor interceptor);
    }
}
