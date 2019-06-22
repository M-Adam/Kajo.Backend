using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Requests
{
    public class DeleteChecklistRequest : RequestBase
    {
        public string ChecklistId { get; set; }
    }
}
