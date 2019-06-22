using System;
using System.Collections.Generic;
using System.Text;
using Kajo.Backend.Common;
using Kajo.Backend.Functions.Checklists;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Kajo.Backend.Functions.Checklists
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.UseWebJobsStartup<CommonFunctionsStartup>();
        }
    }
}
