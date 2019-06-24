using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Requests
{
    public class DeleteChecklistTaskRequest : RequestBase
    {
        public string ChecklistId { get; set; }
        public string ChecklistTaskId { get; set; }
    }
}
