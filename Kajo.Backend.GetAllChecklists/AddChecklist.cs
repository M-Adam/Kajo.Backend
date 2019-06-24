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
    public class AddChecklist : FunctionBase
    {
        public AddChecklist(IChecklistRepository checklistsRepo, IUserRepository userRepository) : base(checklistsRepo, userRepository)
        {
        }

        [FunctionName(nameof(AddChecklist))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] AddOrUpdateChecklistRequest request)
        {
            var checklists = await ChecklistsRepo.CreateChecklist(request);
            await UserRepo.AddChecklistToUser(checklists.Id, request.Auth, true);
            log.LogInformation("Created checklist {name}-{id}", checklists.Name, checklists.Id);
            return Ok(checklists);
        }
    }
}
