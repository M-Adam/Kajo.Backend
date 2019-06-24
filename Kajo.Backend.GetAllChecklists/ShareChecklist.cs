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
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Kajo.Backend.Functions.Checklists
{
    public class ShareChecklist : FunctionBase
    {
        private readonly IMailSender _mailSender;

        [FunctionName(nameof(ShareChecklist))]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log, [RequestBody] ShareChecklistRequest request)
        {
            await UserRepo.AddChecklistToUser(request.ChecklistId, new Auth
            {
                Email = request.SharedWithEmail
            }, false);
            await _mailSender.ShareChecklist(request.ChecklistId, request.Auth.Email, request.SharedWithEmail);
            return Ok();
        }

        public ShareChecklist(IChecklistRepository checklistsRepo, IUserRepository userRepo, IMailSender mailSender) : base(checklistsRepo, userRepo)
        {
            _mailSender = mailSender;
        }
    }
}
