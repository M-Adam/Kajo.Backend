using MongoDB.Bson;
using MongoDB.Driver;

namespace Kajo.Backend.Common.Repositories
{
    internal static class RepositoryConst
    {
        internal const string ConnectionString = @"mongodb://localhost:27017/kajo";
        internal const string DatabaseName = "kajo";
        internal const string UsersCollectionName = "users";
        internal const string ChecklistsCollectionName = "checklists";
    }
}