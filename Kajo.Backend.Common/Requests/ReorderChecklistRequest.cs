using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Requests
{
    public class ReorderChecklistRequest : RequestBase
    {
        public IList<(string checklistId, int order)> ChecklistsOrder { get; set; }
    }
}
