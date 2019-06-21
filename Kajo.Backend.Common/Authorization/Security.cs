using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Kajo.Backend.Common.Authorization
{
    internal static class Security
    {
        private static readonly TokenValidationParameters TokenValidationParameters;

        static Security()
        {
            const string publicKey = @"MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAkztsY+Q7d03Rzn4tWHGL0Vkl+R5feMPV4bNcrYtwR2G9oVj2Ff72wQciMZDUBQoIRhiPRNGaLlP6zJk2+dPlVDGCjXeqwApUmBTfk972dNKU2f9Muq3WLHZBgNqQP3rvFT2D7wr5eDVIcyY8wHBfCWOeTvBtm77xKQDxKMsEraLwzkKXdwrsPi82HPWG2879NN6STkA+ZVhHJyCcMn++Aq8QeNMWUSpSSheaZGpMQ+96O/SvS+0Jthaq7pbBtJD1Ybf7uDWuvAhgEWO0HrF37dcx3v8kk3e/KQXxNZVaOy2coICJ0FE89y434m8G43Qpo2gCHF4vj97lSAHw1jFzEwIDAQAB";
            var publicKeyArray = Convert.FromBase64String(publicKey);
            var asymmetricKeyParameter = PublicKeyFactory.CreateKey(publicKeyArray);
            var rsaKeyParameters = (RsaKeyParameters)asymmetricKeyParameter;
            var rsaParameters = new RSAParameters
            {
                Modulus = rsaKeyParameters.Modulus.ToByteArrayUnsigned(),
                Exponent = rsaKeyParameters.Exponent.ToByteArrayUnsigned()
            };
            var rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(rsaParameters);

            //ToDo: Set TokenValidationParameters
            TokenValidationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = false,
                RequireSignedTokens = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new RsaSecurityKey(rsa)
            };
        }

        internal static bool VerifyJwtToken(this HttpRequest request, out ClaimsPrincipal principal, out string errorMessage)
        {
            try
            {
                var val = request.Headers.First(x => x.Key == "Authorization").Value.ToString();
                var result = new JwtSecurityTokenHandler().ValidateToken(val, TokenValidationParameters, out var validated);
                errorMessage = null;
                principal = result;
                return true;
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
                principal = null;
                return false;
            }
        }
    }
}
