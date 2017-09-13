using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Unicorn.Providers
{
    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

        public string GetOriginalName(HttpContentHeaders headers) => headers.ContentDisposition.FileName.Replace("\"", string.Empty);

        public override string GetLocalFileName(HttpContentHeaders headers) => $"{Guid.NewGuid().ToString()}{Path.GetExtension(GetOriginalName(headers))}";
    }
}