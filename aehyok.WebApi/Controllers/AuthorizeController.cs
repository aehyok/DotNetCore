using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using aehyok.Core.Data;
using aehyok.Model.Authorization;
using aehyok.NLog;
using aehyok.Contracts;
using aehyok.ViewModel;

namespace aehyok.WebApi.Controllers
{
    /// <summary>
    /// ��Ȩ����Api
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthorizeController : BaseController
    {
        private static LogWriter _logger = new LogWriter();

        private readonly IRepository<Role, int> _roleRepository;

        private readonly IRepository<Menu, int> _menuRepository;

        private readonly IRepository<RoleMenu, int> _roleMenuRepository;

        private readonly IAuthorizationContract _authorizationService;

        private readonly IRepository<Organize, int> _organizeRepository;

        private readonly IRepository<Post, int> _postRepository;

        private readonly IRepository<UserPost, int> _userPostRepository;

        private readonly IRepository<PostRole, int> _postRoleRepository;

        public AuthorizeController(IRepository<Role, int> roleRepository,
            IRepository<Menu, int> menuRepository,
            IRepository<Organize, int> organizeRepository,
            IRepository<RoleMenu, int> roleMenuRepository,
            IAuthorizationContract authorizationService,
            IRepository<Post, int> postRepository,
            IRepository<UserPost, int> userPostRepository,
            IRepository<PostRole, int> postRoleRepository)
        {
            this._roleRepository = roleRepository;
            this._menuRepository = menuRepository;
            this._organizeRepository = organizeRepository;
            this._roleMenuRepository = roleMenuRepository;
            this._authorizationService = authorizationService;
            this._postRepository = postRepository;
            this._userPostRepository = userPostRepository;
            this._postRoleRepository = postRoleRepository;
        }


        #region Role��ɫ����
        [HttpGet]
        [Route("Role")]
        public List<Role> GetRoleList()
        {
            var list = _roleRepository.Entities.ToList();
            //return Json(new { data = list });
            return list;
        }

        /// <summary>
        /// ͨ����ɫID��ȡ��ɫ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Role/{id:int}")]
        public dynamic GetRoleById(int id = 0)
        {
            var role = _roleRepository.Entities.FirstOrDefault(item => item.Id == id);

            return Json(role);
        }
        /// <summary>
        /// ɾ����ɫ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Role/{id:int}")]
        public async Task DeleteBlogArticle(int id)
        {
            await _roleRepository.DeleteAsync(id);
        }

        /// <summary>
        /// �����ɫ
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Role")]
        public async Task SaveRole(Role role)
        {
            try
            {
                if (role.Id > 0)
                {
                    await _roleRepository.UpdateAsync(role);
                }
                else
                {
                    await _roleRepository.InsertAsync(role);
                }
            }
            catch (Exception exception)
            {
                _logger.Error("��ɫ����ʱ�����쳣", exception);
            }
        }

        /// <summary>
        /// ��ȡ��ɫ��Ӧ�Ĺ�ѡ�˵�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RoleMenu")]
        public dynamic GetRoleMenuList()
        {
            int roleId = 0;
            //int roleId = int.Parse((((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["roleId"]);
            //var id = (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["id"];
            var id = "0";
            int fartherId = string.IsNullOrEmpty(id) ? 0 : int.Parse(id);
            var isExist = _menuRepository.Entities.Any(item => item.FatherId == fartherId);
            if (isExist)
            {
                var list = _menuRepository.Entities.Where(item => item.Id > 0)
                    .Select(item => new MenuViewModel()
                    {
                        id = item.Id,
                        @checked = (from roleMenu in this._roleMenuRepository.Entities
                                    where roleMenu.RoleId == roleId && roleMenu.MenuId == item.Id
                                    select roleMenu.Id).FirstOrDefault() > 0,
                        name = item.Title,
                        pId = item.FatherId,
                        isParent = true
                    });
                return Json(list);
            }
            else
            {
                return Json("");
            }
        }

        /// <summary>
        /// �����ɫ�˵�Ȩ���б�
        /// </summary>
        /// <param name="roleMenu"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveRoleMenu")]
        public async Task SaveRoleMenu(RoleMenuViewModel roleMenu)
        {
            try
            {
                await this._authorizationService.SaveRoleMenu(roleMenu);
            }
            catch (Exception exception)
            {
                _logger.Error("��ɫ�˵��б���ʱ�����쳣", exception);
            }
        }

        #endregion

        #region Menu�˵�����
        [HttpGet]
        [Route("Menu")]
        public IActionResult GetMenuList()
        {
            //var id = (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["id"];
            int id = 0;
            //int fartherId = string.IsNullOrEmpty(id) ? 0 : int.Parse(id);
            int fatherId = 10;
            var list = _menuRepository.Entities.Where(item => item.FatherId == fatherId).ToList();

            var rootList = list
                .Select(item => new MenuViewModel()
                {
                    id = item.Id,
                    name = item.Title,
                    pId = item.FatherId,
                    isParent = true
                }).ToList();
            return Json(rootList);
        }

        /// <summary>
        /// ɾ���˵�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Menu/{id:int}")]
        public async Task DeleteMenu(int id)
        {
            await _menuRepository.DeleteAsync(id);
        }

        /// <summary>
        /// ����˵�
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Menu")]
        public async Task SaveMenu([FromBody]Menu menu)
        {
            var fatherId = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["FatherId"];
            menu.FatherId = int.Parse(fatherId);
            try
            {
                if (menu.Id > 0)
                {
                    await _menuRepository.UpdateAsync(menu);
                }
                else
                {
                    await _menuRepository.InsertAsync(menu);
                }
            }
            catch (Exception exception)
            {
                _logger.Error("�˵����ݱ���ʱ�����쳣", exception);
            }
        }

        /// <summary>
        /// ͨ���˵�ID��ȡ�˵���Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Menu/{id:int}")]
        public IActionResult GetMenuById(int id = 0)
        {
            var menu = _menuRepository.Entities.FirstOrDefault(item => item.Id == id);

            return Json(menu);
        }

        /// <summary>
        /// ͨ�������˵�ID��ȡ�����б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Menu2")]
        public IActionResult GetMenuList2()
        {
            var id = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["nodeId"];
            if (string.IsNullOrEmpty(id))
            {
                id = "1";
            }
            int fatherId = int.Parse(id);
            var list = _menuRepository.Entities.Where(item => item.FatherId == fatherId).ToList();
            return Json(new { data = list });
        }

        /// <summary>
        /// ͨ���˵�ID����ȡ�˵��µ��б�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Menu3/{id:int}")]
        public IActionResult GetMenuList3(int id)
        {
            var list = _menuRepository.Entities;
            var rootList = list.Where(item => item.Id == id);
            var temp = rootList
                .Select(item => new MenuViewModel2()
                {
                    id = item.Id.ToString(),
                    text = item.Title,
                    parent = item.FatherId.ToString(),
                    children = true
                }).ToList();
            return Json(temp);
        }
        #endregion

        #region Organize��֯��������
        [HttpGet]
        [Route("Organize")]
        public IActionResult GetOrganizeList()
        {
            var id = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["id"];
            int fartherId = string.IsNullOrEmpty(id) ? 0 : int.Parse(id);
            var list = _organizeRepository.Entities.Where(item => item.FatherId == fartherId).ToList();
            var rootList = list
                .Select(item => new MenuViewModel()
                {
                    id = item.Id,
                    name = item.Name,
                    pId = item.FatherId,
                    isParent = true
                }).ToList();
            return Json(rootList);
        }

        /// <summary>
        /// ɾ���˵�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Organize/{id:int}")]
        public async Task DeleteOrganize(int id)
        {
            await _organizeRepository.DeleteAsync(id);
        }

        /// <summary>
        /// ����˵�
        /// </summary>
        /// <param name="organize"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Organize")]
        public async Task SaveOrganize([FromBody]Organize organize)
        {
            var fatherId = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["FatherId"];
            organize.FatherId = int.Parse(fatherId);
            try
            {
                if (organize.Id > 0)
                {
                    await _organizeRepository.UpdateAsync(organize);
                }
                else
                {
                    await _organizeRepository.InsertAsync(organize);
                }
            }
            catch (Exception exception)
            {
                _logger.Error("��֯��������ʱ�����쳣", exception);
            }
        }

        /// <summary>
        /// ͨ����֯ID��ȡ��֯������Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Organize/{id:int}")]
        public IActionResult GetOrganizeById(int id = 0)
        {
            var organize = _organizeRepository.Entities.FirstOrDefault(item => item.Id == id);

            return Json(organize);
        }

        /// <summary>
        /// ��ȡ�����µ���֯����
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Organize2")]
        public IActionResult GetOrganizeList2()
        {
            var id = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["nodeId"];
            if (string.IsNullOrEmpty(id))
            {
                id = "1";
            }
            int fatherId = int.Parse(id);
            var list = _organizeRepository.Entities.Where(item => item.FatherId == fatherId).ToList();
            return Json(new { data = list });
        }

        /// <summary>
        /// ������֯ID��ȡ�˵��б�
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Organize3/{id:int}")]
        public IActionResult GetOrganizeList3(int id)
        {
            var list = _organizeRepository.Entities;
            var rootList = list.Where(item => item.Id == id);
            var temp = rootList
                .Select(item => new MenuViewModel2()
                {
                    id = item.Id.ToString(),
                    text = item.Name,
                    parent = item.FatherId.ToString(),
                    children = true
                }).ToList();
            return Json(temp);
        }
        #endregion

        #region Post��λ����
        /// <summary>
        /// ɾ����λ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Post/{id:int}")]
        public async Task DeletePost(int id)
        {
            await _postRepository.DeleteAsync(id);
        }

        /// <summary>
        /// �����λ
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Post")]
        public async Task SavePost([FromBody]Post post)
        {
            var organizeId = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["organizeId"];
            //post.FatherId = int.Parse(fatherId);
            post.OrganizeId = int.Parse(organizeId);
            try
            {
                if (post.Id > 0)
                {
                    await _postRepository.UpdateAsync(post);
                }
                else
                {
                    await _postRepository.InsertAsync(post);
                }
            }
            catch (Exception exception)
            {
                _logger.Error("��λ����ʱ�����쳣", exception);
            }
        }

        /// <summary>
        /// ��ȡ��֯ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Post/{id:int}")]
        public IActionResult GetPostById(int id = 0)
        {
            var organize = _postRepository.Entities.FirstOrDefault(item => item.Id == id);

            return Json(organize);
        }

        /// <summary>
        /// ��֯�����µĸ�λ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("OrganizePost")]
        public IActionResult GetPostListByOrganizeId()
        {
            var organizeId = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["organizeId"];
            if (string.IsNullOrEmpty(organizeId))
            {
                organizeId = "1";  //Ĭ�ϸ��ڵ㣨���뱣֤�����ݿ�һ�£�
            }
            int fatherId = int.Parse(organizeId);
            var list = _postRepository.Entities.Where(item => item.OrganizeId == fatherId).ToList();
            return Json(new { data = list });
        }

        /// <summary>
        /// ���ݸ�λID��ȡ��λ��Ϣ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Post3/{id:int}")]
        public IActionResult GetPostList3(int id)
        {
            var list = _postRepository.Entities;
            var rootList = list.Where(item => item.Id == id);
            var temp = "";
            return Json(temp);
        }

        /// <summary>
        /// ��ȡ��λ�µĽ�ɫ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("PostRole")]
        public IActionResult GetPostRoleList()
        {
            var postId = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["postId"];
            int id = int.Parse(postId);
            var list = _roleRepository.Entities.Where(item => item.Id > 0)
                .Select(item => new RoleViewModel
                {
                    RoleId = item.Id,
                    Checked = (from postRole in this._postRoleRepository.Entities
                               where postRole.RoleId == item.Id && postRole.PostId == id
                               select postRole.Id).FirstOrDefault() > 0,
                    RoleName = item.Name
                });
            return Json(list);
        }

        /// <summary>
        /// �����λ��ɫ�б�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePostRole")]
        public async Task SavePostRole([FromBody] OneToMany model)
        {
            try
            {
                await this._authorizationService.SavePostRole(model);
            }
            catch (Exception exception)
            {
                _logger.Error("�û���λ��ɫ�б���ʱ�����쳣", exception);
            }
        }
        #endregion

        #region Users �û�����

        /// <summary>
        /// ��ȡ�û������õĸ�λ�б�
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("UserPost")]
        public IActionResult GetUserPostList()
        {
            var userId = "0";// (((HttpContextBase)Request.Properties["MS_HttpContext"])).Request.QueryString["userId"];
            int id = int.Parse(userId);
            var list = from userPost in _userPostRepository.Entities
                       join post in _postRepository.Entities
                           on userPost.PostId equals post.Id
                       join organize in _organizeRepository.Entities
                       on post.OrganizeId equals organize.Id
                       where userPost.UserId == id
                       select new
                       {
                           Id = userPost.Id,
                           PostName = post.Name,
                           OrganizeName = organize.Name
                       };
            return Json(new { data = list });
        }

        /// <summary>
        /// ɾ���û���λ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("UserPost/{id:int}")]
        public async Task DeleteUserPost(int id)
        {
            await _userPostRepository.DeleteAsync(id);
        }

        /// <summary>
        /// �����û���λ�б�
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveUserPost")]
        public async Task SaveRoleMenu([FromBody] OneToMany model)
        {
            try
            {
                await this._authorizationService.SaveUserPost(model);
            }
            catch (Exception exception)
            {
                _logger.Error("�û���λ�б���ʱ�����쳣", exception);
            }
        }
        #endregion
    }
}