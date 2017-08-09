using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unicorn.Shared.services.interception;
using Unicorn.Shared.services.interfaces;

namespace Unicorn.Shared.services
{
    class HttpWrapClientService : IHttpWrapClient, IDisposable
    {
        private const string UserAgent = "Httwrap";
        private readonly IHttpWrapConfiguration _configuration;
        private readonly ICollection<IHttpInterceptor> _interceptors;

        private readonly Action<HttpStatusCode, string> _defaultErrorHandler = (statusCode, body) =>
        {
            if (statusCode < HttpStatusCode.OK || statusCode >= HttpStatusCode.BadRequest)
            {
                throw new HttpWrapHttpException(statusCode, body);
            }
        };

        private readonly HttpClient _httpClient;

        private readonly IQueryStringBuilder _queryStringBuilder;

        public HttpWrapClientService(IHttpWrapConfiguration configuration)
            : this(configuration, new QueryStringBuilder())
        {
        }

        internal HttpWrapClientService(IHttpWrapConfiguration configuration, IQueryStringBuilder queryStringBuilder)
        {
            _configuration = configuration;
            _queryStringBuilder = queryStringBuilder;
            _httpClient = _configuration.GetHttpClient();
            _interceptors = new List<IHttpInterceptor>();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<IHttpWrapResponse> GetAsync(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return await RequestAsync(HttpMethod.Get, path, null, errorHandler, customHeaders, requestTimeout);
        }


        public async Task<IHttpWrapResponse> GetAsync(string path, object payload,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            path = $"{path}?{_queryStringBuilder.BuildFrom(payload)}";

            return await RequestAsync(HttpMethod.Get, path, null, errorHandler, customHeaders, requestTimeout);
        }

        public async Task<IHttpWrapResponse<T>> GetAsync<T>(string path,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return await RequestAsync<T>(HttpMethod.Get, path, null, errorHandler, customHeaders, requestTimeout);
        }

        public async Task<IHttpWrapResponse<T>> GetAsync<T>(string path, object payload,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            path = $"{path}?{_queryStringBuilder.BuildFrom(payload)}";
            return await RequestAsync<T>(HttpMethod.Get, path, null, errorHandler, customHeaders, requestTimeout);
        }

        public IHttpWrapResponse Get(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return Request(HttpMethod.Get, path, null, errorHandler, customHeaders, requestTimeout);
        }
        public IHttpWrapResponse Get(string path, object payload, Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            path = $"{path}?{_queryStringBuilder.BuildFrom(payload)}";

            return Request(HttpMethod.Get, path, null, errorHandler, customHeaders, requestTimeout);
        }

        public async Task<IHttpWrapResponse> PutAsync<T>(string path, T data,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return await RequestAsync(HttpMethod.Put, path, data, errorHandler, customHeaders, requestTimeout);
        }

        public IHttpWrapResponse Put<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return Request(HttpMethod.Put, path, data, errorHandler, customHeaders, requestTimeout);
        }

        public async Task<IHttpWrapResponse> PostAsync<T>(string path, T data,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return await RequestAsync(HttpMethod.Post, path, data, errorHandler, customHeaders, requestTimeout);
        }

        public IHttpWrapResponse Post<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return Request(HttpMethod.Post, path, data, errorHandler, customHeaders, requestTimeout);
        }

        public async Task<IHttpWrapResponse> DeleteAsync(string path, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return await RequestAsync(HttpMethod.Delete, path, null, errorHandler, customHeaders, requestTimeout);
        }

        public IHttpWrapResponse Delete(string path, Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return Request(HttpMethod.Delete, path, null, errorHandler, customHeaders, requestTimeout);

        }

        public async Task<IHttpWrapResponse> PatchAsync<T>(string path, T data,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return await RequestAsync(new HttpMethod("PATCH"), path, data, errorHandler, customHeaders, requestTimeout);
        }

        public IHttpWrapResponse Patch<T>(string path, T data, Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            return Request(new HttpMethod("PATCH"), path, data, errorHandler, customHeaders, requestTimeout);
        }

        public void AddInterceptor(IHttpInterceptor interceptor)
        {
            _interceptors.Add(interceptor);
        }

        private IHttpWrapResponse Request(HttpMethod method, string path, object body,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            var response = RequestImpl(requestTimeout, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None, method,
                path, body, customHeaders);

            var content = response.Content.ReadAsStringAsync().Result;

            HandleIfErrorResponse(response.StatusCode, content, errorHandler);

            return new HttpWrapResponse(response.StatusCode, content);
        }

        private IHttpWrapResponse Request<T>(HttpMethod method, string path, object body,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            var response = RequestImpl(requestTimeout, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None, method,
                path, body, customHeaders);

            var content = response.Content.ReadAsStringAsync().Result;

            HandleIfErrorResponse(response.StatusCode, content, errorHandler);

            return new HttpWrapResponse(response.StatusCode, content);
        }

        private async Task<IHttpWrapResponse> RequestAsync(HttpMethod method, string path, object body,
            Action<HttpStatusCode, string> errorHandler = null, Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            var response =
                await
                    RequestAsyncImpl(requestTimeout, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None, method,
                        path, body, customHeaders);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            HandleIfErrorResponse(response.StatusCode, content, errorHandler);

            return new HttpWrapResponse(response.StatusCode, content);
        }

        private async Task<IHttpWrapResponse<T>> RequestAsync<T>(HttpMethod method, string path,
            object body, Action<HttpStatusCode, string> errorHandler = null,
            Dictionary<string, string> customHeaders = null, TimeSpan? requestTimeout = null)
        {
            var response =
                await
                    RequestAsyncImpl(requestTimeout, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None, method,
                        path, body, customHeaders);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            HandleIfErrorResponse(response.StatusCode, content, errorHandler);

            return new HttpWrapResponse<T>(response)
            {
                Data = _configuration.Serializer.Deserialize<T>(content)
            };
        }

        private HttpResponseMessage RequestImpl(TimeSpan? requestTimeout,
            HttpCompletionOption completionOption, CancellationToken cancellationToken, HttpMethod method,
            string path, object body, Dictionary<string, string> customHeaders = null)
        {
            try
            {
                if (requestTimeout.HasValue)
                {
                    _httpClient.Timeout = requestTimeout.Value;
                }

                var request = PrepareRequest(method, body, path, customHeaders);

                foreach (IHttpInterceptor interceptor in _interceptors)
                {
                    interceptor.OnRequest(request);
                }

                HttpResponseMessage response = _httpClient.SendAsync(request, completionOption, cancellationToken).Result;

                foreach (IHttpInterceptor interceptor in _interceptors)
                {
                    interceptor.OnResponse(request, response);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new HttpWrapException(
                    $"An error occured while execution request. Path : {path} , HttpMethod : {method}", ex);
            }
        }

        private async Task<HttpResponseMessage> RequestAsyncImpl(TimeSpan? requestTimeout,
            HttpCompletionOption completionOption, CancellationToken cancellationToken, HttpMethod method,
            string path, object body, Dictionary<string, string> customHeaders = null)
        {
            try
            {
                if (requestTimeout.HasValue)
                {
                    _httpClient.Timeout = requestTimeout.Value;
                }

                var request = PrepareRequest(method, body, path, customHeaders);

                foreach (IHttpInterceptor interceptor in _interceptors)
                {
                    interceptor.OnRequest(request);
                }

                HttpResponseMessage response = await _httpClient.SendAsync(request, completionOption, cancellationToken);

                foreach (IHttpInterceptor interceptor in _interceptors)
                {
                    interceptor.OnResponse(request, response);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new HttpWrapException(
                    $"An error occured while execution request. Path : {path} , HttpMethod : {method}", ex);
            }
        }

        private HttpRequestMessage PrepareRequest(HttpMethod method, object body, string path,
            Dictionary<string, string> customHeaders = null)
        {
            //var url = $"{_configuration.BasePath}{path}";
            string url;
            if (path.StartsWith("http://") || path.StartsWith("https://"))
            {
                url = path;
            }
            else
            {
                url = _configuration.BasePath + path;
            }

            var request = new HttpRequestMessage(method, url);

            request.Headers.Add("User-Agent", UserAgent);

            request.Headers.Add("Accept", "application/json");

            if (customHeaders != null)
                foreach (var header in customHeaders) request.Headers.Add(header.Key, header.Value);

            if (body != null)
            {
                var content = new JsonRequestContent(body, _configuration.Serializer);
                var requestContent = content.GetContent();
                request.Content = requestContent;
            }

            return request;
        }

        private void HandleIfErrorResponse(HttpStatusCode statusCode, string content,
            Action<HttpStatusCode, string> errorHandler)
        {
            if (errorHandler != null)
            {
                errorHandler(statusCode, content);
            }
            else
            {
                _defaultErrorHandler(statusCode, content);
            }
        }
    }
}