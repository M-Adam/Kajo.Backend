using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Kajo.Backend.Functions.ChecklistTasks
{
    public static class ReorderChecklistTask
    {
        [FunctionName(nameof(ReorderChecklistTask))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = null)] HttpRequest req,
            ILogger log)
        {
            throw new NotImplementedException();
        }
    }
}
