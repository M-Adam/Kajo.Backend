using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Kajo.Backend.Common.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Newtonsoft.Json;

namespace Kajo.Backend.Common.RequestObjectReader
{
    internal class RequestBodyValueProvider : IValueProvider
    {
        private readonly HttpRequest _request;

        public RequestBodyValueProvider(HttpRequest request, Type type)
        {
            _request = request;
            Type = type;
        }

        public async Task<object> GetValueAsync()
        {
            var requestBody = await _request.ReadAsStringAsync();
            return JsonConvert.DeserializeObject(requestBody, Type);
        }

        public Type Type { get; }

        public string ToInvokeString() => string.Empty;
    }
}