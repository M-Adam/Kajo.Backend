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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private static FilterDefinition<User> GetUserFilterDefinition(Auth auth)
            => Filter.Or(Filter.Eq(x => x.Id, auth.Id), Filter.Eq(x => x.Email, auth.Email));
        

        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, RepositoryConst.UsersCollectionName)
        {
        }

        public async Task AddChecklistToUser(string checklistsId, Auth requestAuth, bool isOwned)
        {
            var user = await GetUser(requestAuth);
            var update = Update.AddToSet(x => x.Checklists, new UsersChecklist
            {
                ChecklistId = checklistsId,
                Order = user.Checklists.Max(y=>y.Order) + 1,
                IsOwned = isOwned
            });

            var filter = Filter.Eq(x => x.Email, requestAuth.Email);
            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteChecklistOwnership(DeleteChecklistRequest request)
        {
            var filter = Filter.Eq(x => x.Id, request.Auth.Id);
            var update = Update.PullFilter(x => x.Checklists, x=>x.ChecklistId == request.ChecklistId && x.IsOwned);
            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task<User> GetUser(Auth auth, bool createIfNotExists = false)
        {
            var user = await Collection.Find(GetUserFilterDefinition(auth)).FirstOrDefaultAsync();
            if (user != null)
            {
                return user;
            }

            if (createIfNotExists)
            {
                user = new User()
                {
                    Email = auth.Email,
                    Id = auth.Id
                };
                await Collection.InsertOneAsync(user);
            }
            
            return user;
        }

        public async Task ReorderChecklist(ReorderChecklistRequest request)
        {
            var user = await GetUser(request.Auth);
            foreach (var checklist in user.Checklists)
            {
                checklist.Order = request.ChecklistsOrder.First(x => x.checklistId == checklist.ChecklistId.ToString()).order;
            }

            var update = Update.Combine(new List<UpdateDefinition<User>>()
            {
                Update.PullFilter(x => x.Checklists, x => true),
                Update.PushEach(x => x.Checklists, user.Checklists)
            });

            await Collection.UpdateOneAsync(GetUserFilterDefinition(request.Auth), update);
        }

        public async Task<bool> HasAccessToChecklist(string checklistId, Auth requestAuth)
        {
            var user = await GetUser(requestAuth);
            return user.Checklists.Any(x => x.ChecklistId == checklistId);
        }
    }

    public interface IUserRepository
    {
        Task AddChecklistToUser(string checklistsId, Auth requestAuth, bool isOwner);
        Task DeleteChecklistOwnership(DeleteChecklistRequest request);
        Task<User> GetUser(Auth auth, bool createIfNotExists = false);
        Task ReorderChecklist(ReorderChecklistRequest request);
        Task<bool> HasAccessToChecklist(string checklistId, Auth requestAuth);
    }
}
