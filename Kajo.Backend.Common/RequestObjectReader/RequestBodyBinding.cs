using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace Kajo.Backend.Common.RequestObjectReader
{
    internal class RequestBodyBinding : IBinding
    {
        private readonly Type _parameterType;

        public RequestBodyBinding(Type parameterType)
        {
            _parameterType = parameterType;
        }

        public async Task<IValueProvider> BindAsync(BindingContext context)
        {
            var request = context.BindingData["req"] as HttpRequest;
            return await Task.FromResult<IValueProvider>(new RequestBodyValueProvider(request, _parameterType));
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context) => null;

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();
    }
}