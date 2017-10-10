using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.WebApi
{
    public class AuthorizeBearer:AuthorizeAttribute
    {
        public AuthorizeBearer()
        {
            
        }
    }
}
