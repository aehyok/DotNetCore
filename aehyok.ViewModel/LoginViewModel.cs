using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.ViewModel
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Ticket { get; set; }

        public bool RememberMe { get; set; }
    }
}
