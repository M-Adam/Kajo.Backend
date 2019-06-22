using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kajo.Backend.Common.Authorization;
using Kajo.Backend.Common.Models;
using Kajo.Backend.Common.Requests;
using MongoDB.Driver;

namespace Kajo.Backend.Common.Repositories
{
    public class ChecklistsRepository : BaseRepository<Checklist>, IChecklistRepository
    {
        private static FilterDefinitionBuilder<Checklist> Filter => Builders<Checklist>.Filter;

        public ChecklistsRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, RepositoryConst.ChecklistsCollectionName)
        {
        }

        public async Task<List<Checklist>> GetChecklistsAvailableToUser(string email)
        {
            var ownerDefinition = Filter.Eq(x => x.ChecklistOwner.Email, email);
            var sharedDefinition = Filter.ElemMatch(x => x.SharedWith, x=>x.Email == email);
            var filter = Filter.Or(ownerDefinition, sharedDefinition);
            var checklists = await GetWhere(filter);
            return checklists;
        }

        public async Task<Checklist> CreateChecklist(AddOrUpdateChecklistRequest request, Auth auth)
        {
            var currentCount = await Collection.CountDocumentsAsync(Filter.Eq(x => x.ChecklistOwner.Email, auth.Email));
            var checklist = new Checklist
            {
                Name = request.Name,
                Description = request.Description,
                ChecklistOwner = new ChecklistUser()
                {
                    Email = auth.Email,
                    Id = auth.Id
                },
                Order = (int)currentCount + 1,
                IsCurrentUserTheOwner = true
            };
            await Collection.InsertOneAsync(checklist);
            return checklist;
        }
    }

    public interface IChecklistRepository 
    {
        Task<List<Checklist>> GetChecklistsAvailableToUser(string email);
        Task<Checklist> CreateChecklist(AddOrUpdateChecklistRequest request, Auth auth);
    }
}
