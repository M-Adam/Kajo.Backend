using System;
using System.IO;
using System.Threading.Tasks;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.RequestObjectReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kajo.Backend.Functions.Checklists
{
    public static class DeleteChecklist
    {
        [FunctionName(nameof(DeleteChecklist))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log, [AccessToken] Auth auth, [RequestBody] DeleteChecklistRequest request)
        {
            throw new NotImplementedException();
        }
    }

    public class DeleteChecklistRequest
    {
        public string ChecklistId { get; set; }
    }
}
