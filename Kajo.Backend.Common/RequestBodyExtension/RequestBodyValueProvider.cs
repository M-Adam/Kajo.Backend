using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509.Qualified;

namespace Kajo.Backend.Common.RequestBodyExtension
{
    internal class RequestBodyValueProvider : IValueProvider
    {
        private static readonly Type RequestBaseType = typeof(RequestBase);
        private readonly HttpRequest _request;

        public RequestBodyValueProvider(HttpRequest request, Type type)
        {
            _request = request;
            Type = type;
        }

        public async Task<object> GetValueAsync()
        {
            var body = await _request.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject(body, Type);
            if (Type.IsAssignableFrom(RequestBaseType))
            {
                var verificationResult = _request.VerifyJwtToken(out var principal, out var errorMessage);
                if (obj == null)
                {
                    obj = new RequestBase();
                }
                if (verificationResult)
                {
                    ((RequestBase)obj).Auth = new Auth
                    {
                        Id = principal.FindFirst(ClaimTypes.NameIdentifier).Value,
                        Email = principal.FindFirst(ClaimTypes.Email).Value,
                        IsAuthenticated = principal.Identity.IsAuthenticated
                    };
                }
                else
                {
                    //throw new AuthenticationException(errorMessage);
                    //todo: add function proxy for auth purposes
                    ((RequestBase)obj).Auth = new Auth
                    {
                        IsAuthenticated = true,
                        ErrorMessage = errorMessage,
                        Email = "test@test.com",
                        Id = "testUserId"
                    };
                }
            }

            return obj;
        }

        public Type Type { get; }

        public string ToInvokeString() => string.Empty;
    }
}