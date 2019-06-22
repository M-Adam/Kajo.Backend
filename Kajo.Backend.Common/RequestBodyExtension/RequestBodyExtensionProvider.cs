using Microsoft.Azure.WebJobs.Host.Config;

namespace Kajo.Backend.Common.RequestBodyExtension
{
    internal class RequestBodyExtensionProvider : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            // Creates a rule that links the attribute to the binding
            var provider = new RequestBodyBindingProvider();
            var rule = context.AddBindingRule<RequestBodyAttribute>().Bind(provider);
            
        }
    }
}
