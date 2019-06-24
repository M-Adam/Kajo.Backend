using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Requests
{
    public class AddChecklistTaskRequest : RequestBase
    {
        public string ChecklistId { get; set; }
        public string Text { get; set; }
        public int Order { get; set; }
    }
}
