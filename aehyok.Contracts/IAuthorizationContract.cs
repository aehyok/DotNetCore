using aehyok.Core;
using aehyok.Model.Authorization;
using aehyok.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Contracts
{
    /// <summary>
    /// 系统授权管理
    /// </summary>
    public interface IAuthorizationContract : IDependency
    {
        /// <summary>
        /// 保存角色菜单权限列表
        /// </summary>
        /// <param name="roleMenu"></param>
        /// <returns></returns>
        Task SaveRoleMenu(RoleMenuViewModel roleMenu);

        Task SaveUserPost(OneToMany model);

        Task SavePostRole(OneToMany model);

        List<Menu> GetUserMenu(int userId);

    }
}
