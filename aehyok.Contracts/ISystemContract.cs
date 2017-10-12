using aehyok.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Contracts
{
    public interface ISystemContract
    {
        List<ApplicationRole> GetRoleList();
    }
}
