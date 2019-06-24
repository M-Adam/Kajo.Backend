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
    public class AddChecklistTask : FunctionBase
    {
        [FunctionName(nameof(AddChecklistTask))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] AddChecklistTaskRequest request)
        {
            var hasAccess = await UserRepo.HasAccessToChecklist(request.ChecklistId, request.Auth);
            if (hasAccess)
            {
                var task = await ChecklistsRepo.AddChecklistTask(request);
                return Ok(task);
            }
            return new ForbidResult();
        }

        public AddChecklistTask(IChecklistRepository checklistsRepo, IUserRepository userRepo) : base(checklistsRepo, userRepo)
        {
        }
    }
}
