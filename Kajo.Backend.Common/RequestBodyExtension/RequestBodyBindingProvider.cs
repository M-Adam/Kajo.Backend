using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace Kajo.Backend.Common.RequestBodyExtension
{
    internal class RequestBodyBindingProvider : IBindingProvider
    {
        public async Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            var parameterType = context.Parameter.ParameterType;
            IBinding binding = new RequestBodyBinding(parameterType);
            return await Task.FromResult(binding);
        }
    }
}
