using System;
using Microsoft.Azure.WebJobs.Description;

namespace Kajo.Backend.Common.RequestObjectReader
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class RequestBodyAttribute : Attribute
    {
    }
}