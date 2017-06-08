using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.IdentityServer.Helper.Consent
{
    public class ConsentInputModel
    {
        public string Button { get; set; }
        public IEnumerable<string> ScopesConsented { get; set; }
        public bool RememberConsent { get; set; }
        public string ReturnUrl { get; set; }
    }
}
