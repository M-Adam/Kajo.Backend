using System;
using System.Collections.Generic;
using System.Text;
using Kajo.Backend.Common.Authorization;

namespace Kajo.Backend.Common.Requests
{
    public class RequestBase
    {
        public Auth Auth { get; set; }
    }
}
