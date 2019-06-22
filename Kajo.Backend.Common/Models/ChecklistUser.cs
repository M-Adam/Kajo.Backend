using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace Kajo.Backend.Common.Models
{
    public class ChecklistUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
