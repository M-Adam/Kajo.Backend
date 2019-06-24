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
    public class ChangeChecklistTaskStatus : FunctionBase
    {
        [FunctionName(nameof(ChangeChecklistTaskStatus))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] ChangeChecklistTaskStatusRequest request)
        {
            if (await UserRepo.HasAccessToChecklist(request.ChecklistId, request.Auth))
            {
                await ChecklistsRepo.ChangeChecklistTaskStatus(request);
                return Ok();
            }
            
            return Unauthorized();
        }

        public ChangeChecklistTaskStatus(IChecklistRepository checklistsRepo, IUserRepository userRepo) : base(checklistsRepo, userRepo)
        {
        }
    }
}
