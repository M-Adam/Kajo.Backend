using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Requests
{
    public class ChangeChecklistTaskStatusRequest : RequestBase
    {
        public string  ChecklistId { get; set; }
        public string ChecklistTaskId { get; set; }
        public bool IsChecked { get; set; }
    }
}
