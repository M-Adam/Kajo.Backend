using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Kajo.Backend.Common.Models
{
    public class UsersChecklist
    {
        public string ChecklistId { get; set; }
        public int Order { get; set; }
        public bool IsOwned { get; set; }
    }
}
