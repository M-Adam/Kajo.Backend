using System;
using Microsoft.Azure.WebJobs.Description;

namespace Kajo.Backend.Common.Authorization
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class AccessTokenAttribute : Attribute
    {
    }
}