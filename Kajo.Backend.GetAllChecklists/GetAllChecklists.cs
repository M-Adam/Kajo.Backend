using System;
using System.IO;
using System.Threading.Tasks;
using Kajo.Backend.Common;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Models;
using Kajo.Backend.Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kajo.Backend.Functions.Checklists
{
    public class GetAllChecklists : FunctionBase
    {
        public GetAllChecklists(IChecklistRepository checklistsRepo) : base(checklistsRepo)
        {
        }
        
        [FunctionName(nameof(GetAllChecklists))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log, [AccessToken] Auth auth)
        {
            if (!auth.IsAuthenticated)
            {
                return Unauthorized();
            }

            var checklists = await ChecklistsRepo.GetChecklistsAvailableToUser(auth.Email);
            return Ok(checklists);
        }
    }
}
