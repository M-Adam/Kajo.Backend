using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Models;
using Kajo.Backend.Common.Requests;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kajo.Backend.Common.Repositories
{
    public class ChecklistsRepository : BaseRepository<Checklist>, IChecklistRepository
    {
        public ChecklistsRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, RepositoryConst.ChecklistsCollectionName)
        {
        }

        public async Task<List<Checklist>> GetChecklistsAvailableToUser(User user)
        {
            var filter = Filter.In(x => x.Id, user.OwnedChecklists.Concat(user.GuestChecklists));
            var checklists = await GetWhere(filter);
            foreach (var checklist in checklists.Where(x=>user.OwnedChecklists.Contains(x.Id)))
            {
                checklist.IsCurrentUserTheOwner = true;
            }
            return checklists;
        }

        public async Task<Checklist> CreateChecklist(AddOrUpdateChecklistRequest request)
        {
            var currentCount = await Collection.CountDocumentsAsync(Filter.Eq(x => x.ChecklistOwner.Email, request.Auth.Email));
            var checklist = new Checklist
            {
                Name = request.Name,
                Description = request.Description,
                ChecklistOwner = new ChecklistUser()
                {
                    Email = request.Auth.Email,
                    Id = request.Auth.Id
                },
                Order = (int)currentCount + 1,
                IsCurrentUserTheOwner = true
            };
            await Collection.InsertOneAsync(checklist);
            return checklist;
        }

        public async Task DeleteChecklist(DeleteChecklistRequest request)
        {
            var filter = Filter.Eq(x => x.Id, ObjectId.Parse(request.ChecklistId));
            await Collection.DeleteOneAsync(filter);
        }
    }

    public interface IChecklistRepository 
    {
        Task<List<Checklist>> GetChecklistsAvailableToUser(User user);
        Task<Checklist> CreateChecklist(AddOrUpdateChecklistRequest request);
        Task DeleteChecklist(DeleteChecklistRequest request);
    }
}
