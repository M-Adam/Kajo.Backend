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

        public FunctionBase(IChecklistRepository checklistsRepo)
        {
            ChecklistsRepo = checklistsRepo;
        }

        protected OkObjectResult Ok(object obj) => new OkObjectResult(obj);
        protected UnauthorizedResult Unauthorized() => new UnauthorizedResult();
    }
}
