using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kajo.Backend.Common.Models
{
    public class Checklist 
    {
        [BsonConstructor]
        public Checklist()
        {
            SharedWith = new List<ChecklistUser>();
            ChecklistTasks = new List<ChecklistTask>();
        }

        [BsonId]
        public ObjectId Id { get; set; }
        public ChecklistUser ChecklistOwner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Order { get; set; }
        public List<ChecklistUser> SharedWith { get; set; }
        public List<ChecklistTask> ChecklistTasks { get; set; }

        [BsonIgnore] public bool IsCurrentUserTheOwner { get; set; }
    }
}
