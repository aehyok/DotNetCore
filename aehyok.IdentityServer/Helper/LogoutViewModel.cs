using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.IdentityServer.Helper
{
    public class LogoutViewModel:LogoutInputModel
    {
        public bool ShowLogoutPrompt { get; set; }
    }
}
