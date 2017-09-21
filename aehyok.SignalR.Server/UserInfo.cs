using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aehyok.Users.SignalR
{
    public class UserInfo
    {
        [Key]
        public string ContextId { get; set; }
        public string Name { get; set; }
    }
}