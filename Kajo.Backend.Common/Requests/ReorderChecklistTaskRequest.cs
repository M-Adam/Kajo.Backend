using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Requests
{
    public class ReorderChecklistTaskRequest : RequestBase
    {
        public string ChecklistId { get; set; }
        public IList<(string taskId, int order)> ChecklistTasksOrdering { get; set; }
    }
}
