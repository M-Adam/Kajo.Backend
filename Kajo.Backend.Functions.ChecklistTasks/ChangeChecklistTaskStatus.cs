using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Kajo.Backend.Functions.ChecklistTasks
{
    public static class ChangeChecklistTaskStatus
    {
        [FunctionName(nameof(ChangeChecklistTaskStatus))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            throw new NotImplementedException();
        }
    }
}
