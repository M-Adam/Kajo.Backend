using System;
using System.IO;
using System.Threading.Tasks;
using Kajo.Backend.Common;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Repositories;
using Kajo.Backend.Common.RequestBodyExtension;
using Kajo.Backend.Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kajo.Backend.Functions.Checklists
{
    public class DeleteChecklist : FunctionBase
    {
        public DeleteChecklist(IChecklistRepository checklistsRepo, IUserRepository userRepository) : base(checklistsRepo, userRepository)
        {
        }

        [FunctionName(nameof(DeleteChecklist))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] DeleteChecklistRequest request)
        {
            await ChecklistsRepo.DeleteChecklist(request);
            await UserRepo.DeleteChecklistOwnership(request);
            log.LogInformation("Removed checklist {id} by {user}", request.ChecklistId, request.Auth);
            return Ok();
        }

        
    }

   
}
