using System;
using System.Collections.Generic;
using System.Text;
using Kajo.Backend.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Kajo.Backend.Common
{
    public class FunctionBase
    {
        protected readonly IChecklistRepository ChecklistsRepo;
        protected readonly IUserRepository UserRepo;

        public FunctionBase(IChecklistRepository checklistsRepo, IUserRepository userRepo)
        {
            ChecklistsRepo = checklistsRepo;
            UserRepo = userRepo;
        }

        protected OkObjectResult Ok(object obj) => new OkObjectResult(obj);
        protected OkResult Ok() => new OkResult();
        protected UnauthorizedResult Unauthorized() => new UnauthorizedResult();
    }
}
