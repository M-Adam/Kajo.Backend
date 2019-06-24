using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Requests
{
    public class ShareChecklistRequest : RequestBase
    {
        public string ChecklistId { get; set; }
        public string SharedWithEmail { get; set; }
    }
}
