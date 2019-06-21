using System;
using System.Collections.Generic;
using System.Text;

namespace Kajo.Backend.Common.Authorization
{
    public sealed class Auth
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ErrorMessage { get; set; }
    }
}
