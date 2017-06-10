using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace aehyok.WebApi.Controllers
{
    /// <summary>
    /// ��Ȩ����Api
    /// </summary>
    [Produces("application/json")]
    [Route("api/Authorize")]
    public class AuthorizeController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// ���캯����ʼ��
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        /// <param name="roleManager"></param>
        public AuthorizeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Test")]
        public async Task<string>  GetAsync()
        {
            await _userManager.AddLoginAsync(null, null);

            await _signInManager.PasswordSignInAsync("", "", true, lockoutOnFailure: false);

            //�����û�
            await _userManager.CreateAsync(null);

            //ɾ���û�
            await _userManager.DeleteAsync(null);

            //�޸��û�
            await _userManager.UpdateAsync(null);

            await _signInManager.CreateUserPrincipalAsync(null);

            //������ɫ
            await _roleManager.CreateAsync(null);

            //ɾ����ɫ
            await _roleManager.DeleteAsync(null);

            //�޸Ľ�ɫ
            await _roleManager.UpdateAsync(null);

            await _roleManager.FindByIdAsync("");

            _roleManager.Roles.ToList();
            return "";
        }
    }
}