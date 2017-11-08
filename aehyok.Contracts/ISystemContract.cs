using aehyok.Core;
using aehyok.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Contracts
{
    public interface ISystemContract: IDependency
    {
        List<ApplicationRole> GetRoleList();

        int GetRoleListCount();


    }
}
