using System.IO;
using System.Threading.Tasks;
using Kajo.Backend.Common;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Repositories;
using Kajo.Backend.Common.RequestObjectReader;
using Kajo.Backend.Common.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Kajo.Backend.Functions.Checklists
{
    public class AddChecklist : FunctionBase
    {
        public AddChecklist(IChecklistRepository checklistsRepo) : base(checklistsRepo)
        {
        }

        [FunctionName(nameof(AddChecklist))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log, [AccessToken] Auth auth, [RequestBody] AddOrUpdateChecklistRequest request)
        {
            if (!auth.IsAuthenticated)
            {
                return Unauthorized();
            }
            
            var checklists = await ChecklistsRepo.CreateChecklist(request, auth);
            log.LogInformation("Created checklist {name}-{id}", checklists.Name, checklists.Id);
            return Ok(checklists);
        }
    }
}
