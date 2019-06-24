﻿using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Kajo.Backend.Common.Models
{
    public class ChecklistTask
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool Done { get; set; }
        public int Order { get; set; }
        //ToDo: Comments, Audit?
    }
}
