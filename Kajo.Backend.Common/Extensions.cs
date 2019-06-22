using System;
using System.Collections.Generic;
using System.Text;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.RequestObjectReader;
using Microsoft.Azure.WebJobs;

namespace Kajo.Backend.Common
{
    public static class Extensions
    {
        public static IWebJobsBuilder AddAccessTokenBinding(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<AccessTokenExtensionProvider>();
            return builder;
        }

        public static IWebJobsBuilder AddRequestBodyBinding(this IWebJobsBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.AddExtension<RequestBodyExtensionProvider>();
            return builder;
        }
    }
}
