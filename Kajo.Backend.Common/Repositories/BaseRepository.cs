using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Kajo.Backend.Common.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kajo.Backend.Common.Repositories
{
    public abstract class BaseRepository<T>
    {
        protected static FilterDefinitionBuilder<T> Filter => Builders<T>.Filter;
        protected static UpdateDefinitionBuilder<T> Update => Builders<T>.Update;
        protected IMongoCollection<T> Collection;
        

        public BaseRepository(IMongoDatabase mongoDatabase, string collectionName)
        {
            Collection = mongoDatabase.GetCollection<T>(collectionName);
        }

        protected async Task<List<T>> GetWhere(FilterDefinition<T> filterDefinition)
        {
            return await Collection.Find(filterDefinition).ToListAsync();
        }
    }
}
