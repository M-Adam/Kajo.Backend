﻿using System;
using Microsoft.Azure.WebJobs.Description;

namespace Kajo.Backend.Common.RequestBodyExtension
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    [Binding]
    public sealed class RequestBodyAttribute : Attribute
    {
    }
}