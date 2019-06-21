using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace Kajo.Backend.Common.Authorization
{
    internal class AccessTokenBindingProvider : IBindingProvider
    {
        public async Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            IBinding binding = new AccessTokenBinding();
            return await Task.FromResult(binding);
        }
    }
}
