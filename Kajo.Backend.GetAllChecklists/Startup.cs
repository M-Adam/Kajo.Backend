using System;
using System.Collections.Generic;
using System.Text;
using Kajo.Backend.Common;
using Kajo.Backend.Functions.Checklists;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Kajo.Backend.Functions.Checklists
{
    public class Startup : CommonFunctionsStartup
    {
        
    }
}
