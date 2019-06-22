using System;
using System.IO;
using System.Threading.Tasks;
using Kajo.Backend.Common.RequestBodyExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kajo.Backend.Functions.Checklists
{
    public static class ReorderChecklist
    {
        [FunctionName(nameof(ReorderChecklist))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] ReorderChecklistRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
