using System;
using System.Collections.Generic;
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
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, RepositoryConst.UsersCollectionName)
        {
        }

        public async Task AddChecklistToUser(ObjectId checklistsId, Auth requestAuth)
        {
            var update = Update.AddToSet(x => x.OwnedChecklists, checklistsId);
            var filterEmail = Filter.Eq(x => x.Email, requestAuth.Email);
            var filterId = Filter.Eq(x => x.Id, requestAuth.Id);
            var filter = Filter.Or(filterId, filterEmail);
            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteChecklistOwnership(DeleteChecklistRequest request)
        {
            var filter = Filter.Eq(x => x.Id, request.Auth.Id);
            var update = Update.Pull(x => x.OwnedChecklists, ObjectId.Parse(request.ChecklistId));
            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task<User> GetUser(Auth auth)
        {
            return await Collection.Find(x => x.Id == auth.Id || x.Email == auth.Email).FirstOrDefaultAsync();
        }
    }

    public interface IUserRepository
    {
        Task AddChecklistToUser(ObjectId checklistsId, Auth requestAuth);
        Task DeleteChecklistOwnership(DeleteChecklistRequest request);
        Task<User> GetUser(Auth auth);
    }
}
