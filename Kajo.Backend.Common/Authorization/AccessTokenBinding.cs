using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace Kajo.Backend.Common.Authorization
{
    internal class AccessTokenBinding : IBinding
    {
        public async Task<IValueProvider> BindAsync(BindingContext context)
        {
            var request = context.BindingData["req"] as HttpRequest;
            return await Task.FromResult<IValueProvider>(new AccessTokenValueProvider(request));
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context) => null;

        public ParameterDescriptor ToParameterDescriptor() => new ParameterDescriptor();
    }
}