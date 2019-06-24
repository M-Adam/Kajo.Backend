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

        public async Task<IOrderedEnumerable<Checklist>> GetChecklistsAvailableToUser(User user)
        {
            var checklists = await GetWhere(Filter.In(x=>x.Id, user.Checklists.Select(x=>x.ChecklistId)));
            
            return checklists.OrderBy(x=>user.Checklists.Find(y=>y.ChecklistId == x.Id).Order);
        }

        public async Task<Checklist> CreateChecklist(AddOrUpdateChecklistRequest request)
        {
            var checklist = new Checklist
            {
                Name = request.Name,
                Description = request.Description,
            };
            await Collection.InsertOneAsync(checklist);
            return checklist;
        }

        public async Task DeleteChecklist(DeleteChecklistRequest request)
        {
            var filter = Filter.Eq(x => x.Id, request.ChecklistId);
            await Collection.DeleteOneAsync(filter);
        }

        public async Task<ChecklistTask> AddChecklistTask(AddChecklistTaskRequest request)
        {
            var task = new ChecklistTask()
            {
                Order = request.Order,
                Id = ObjectId.GenerateNewId().ToString(),
                Text = request.Text
            };
            var update = Update.AddToSet(x => x.ChecklistTasks, task);
            var filter = Filter.Eq(x => x.Id, request.ChecklistId);
            await Collection.UpdateOneAsync(filter, update);
            return task;
        }

        public async Task ChangeChecklistTaskStatus(ChangeChecklistTaskStatusRequest request)
        {
            var filter = Filter.And(Filter.Eq(x => x.Id, request.ChecklistId), Filter.ElemMatch(x=>x.ChecklistTasks, x=>x.Id == request.ChecklistTaskId));
            var checklist = await Collection.Find(filter).FirstAsync();
            var checklistTask = checklist.ChecklistTasks.First(x => x.Id == request.ChecklistTaskId);
            var update = Update.Set("checklistTasks.$.done", request.IsChecked);
            await Collection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteChecklistTask(DeleteChecklistTaskRequest request)
        {
            var filter = Filter.Eq(x => x.Id, request.ChecklistId);
            var update = Update.PullFilter(x => x.ChecklistTasks, x => x.Id == request.ChecklistTaskId);
            await Collection.UpdateOneAsync(filter, update);
        }
    }

    public interface IChecklistRepository 
    {
        Task<IOrderedEnumerable<Checklist>> GetChecklistsAvailableToUser(User user);
        Task<Checklist> CreateChecklist(AddOrUpdateChecklistRequest request);
        Task DeleteChecklist(DeleteChecklistRequest request);
        Task<ChecklistTask> AddChecklistTask(AddChecklistTaskRequest request);
        Task ChangeChecklistTaskStatus(ChangeChecklistTaskStatusRequest request);
        Task DeleteChecklistTask(DeleteChecklistTaskRequest request);
    }
}
