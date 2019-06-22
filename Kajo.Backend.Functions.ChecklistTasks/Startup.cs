using Kajo.Backend.Common;
using Kajo.Backend.Functions.ChecklistTasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Kajo.Backend.Functions.ChecklistTasks
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.UseWebJobsStartup<CommonFunctionsStartup>();
        }
    }
}