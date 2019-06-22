using System;
using System.IO;
using System.Threading.Tasks;
using Kajo.Backend.Common;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Models;
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
    public class GetAllChecklists : FunctionBase
    {
        public GetAllChecklists(IChecklistRepository checklistsRepo, IUserRepository userRepository) : base(checklistsRepo, userRepository)
        {
        }
        
        [FunctionName(nameof(GetAllChecklists))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] RequestBase request)
        {
            var user = await UserRepo.GetUser(request.Auth);
            var checklists = await ChecklistsRepo.GetChecklistsAvailableToUser(user);
            return Ok(checklists);
        }
    }
}
