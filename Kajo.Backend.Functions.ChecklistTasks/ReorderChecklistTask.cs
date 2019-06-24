using System;
using System.Threading.Tasks;
using Kajo.Backend.Common;
using Kajo.Backend.Common.Models;
using Kajo.Backend.Common.Repositories;
using Kajo.Backend.Common.RequestBodyExtension;
using Kajo.Backend.Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Kajo.Backend.Functions.ChecklistTasks
{
    public class ReorderChecklistTask : FunctionBase
    {
        [FunctionName(nameof(ReorderChecklistTask))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] ReorderChecklistTaskRequest request)
        {
            if (await UserRepo.HasAccessToChecklist(request.ChecklistId, request.Auth))
            {
                await ChecklistsRepo.ReorderChecklistTasks(request);
                log.LogInformation("Checklists {id} tasks reordered", request.ChecklistId);
                return Ok();
            }

            return Unauthorized();
        }

        public ReorderChecklistTask(IChecklistRepository checklistsRepo, IUserRepository userRepo) : base(checklistsRepo, userRepo)
        {
        }
    }
}
