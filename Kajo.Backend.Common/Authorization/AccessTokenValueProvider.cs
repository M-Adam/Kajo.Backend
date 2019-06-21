using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace Kajo.Backend.Common.Authorization
{
    internal class AccessTokenValueProvider : IValueProvider
    {
        private readonly HttpRequest _request;

        public AccessTokenValueProvider(HttpRequest request)
        {
            _request = request;
        }

        public Task<object> GetValueAsync()
        {
            var verificationResult = _request.VerifyJwtToken(out var principal, out var errorMessage);
            Auth result;
            if (verificationResult)
            {
                result = new Auth
                {
                    Id = principal.FindFirst(ClaimTypes.NameIdentifier).Value,
                    Email = principal.FindFirst(ClaimTypes.Email).Value,
                    IsAuthenticated = principal.Identity.IsAuthenticated
                };
            }
            else
            {
                result = new Auth
                {
                    IsAuthenticated = false,
                    ErrorMessage = errorMessage
                };
            }
            
            return Task.FromResult((object)result);
        }

        public Type Type => typeof(Auth);

        public string ToInvokeString() => string.Empty;
    }
}