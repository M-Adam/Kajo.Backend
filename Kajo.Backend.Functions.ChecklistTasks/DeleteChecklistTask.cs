using System;
using System.Threading.Tasks;
using Kajo.Backend.Common;
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
    public class DeleteChecklistTask : FunctionBase
    {
        [FunctionName(nameof(DeleteChecklistTask))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] DeleteChecklistTaskRequest request)
        {
            if (await UserRepo.HasAccessToChecklist(request.ChecklistId, request.Auth))
            {
                await ChecklistsRepo.DeleteChecklistTask(request);
                log.LogInformation("Checklist {id} task {id2} deleted", request.ChecklistId, request.ChecklistTaskId);
                return Ok();
            }

            return Unauthorized();
        }

        public DeleteChecklistTask(IChecklistRepository checklistsRepo, IUserRepository userRepo) : base(checklistsRepo, userRepo)
        {
        }
    }
}
