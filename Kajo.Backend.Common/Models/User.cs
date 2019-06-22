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
            OwnedChecklists = new List<ObjectId>();
            GuestChecklists = new List<ObjectId>();
        }

        [BsonId]
        public string Id { get; set; }
        public string Email { get; set; }
        public List<ObjectId> OwnedChecklists { get; set; }
        public List<ObjectId> GuestChecklists { get; set; }
    }
}
