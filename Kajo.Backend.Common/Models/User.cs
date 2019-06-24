using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kajo.Backend.Common.Models
{
    public class User
    {
        [BsonConstructor]
        public User()
        {
            Checklists = new List<UsersChecklist>();
        }

        [BsonId]
        public string Id { get; set; }
        public string Email { get; set; }
        public List<UsersChecklist> Checklists { get; set; }
    }
}
