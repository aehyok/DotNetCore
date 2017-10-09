using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using aehyok.Core.Data;
using aehyok.Model.Blog;
using aehyok.Contracts;
using aehyok.NLog;
using aehyok.WebApi.ViewModel;
using aehyok.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace aehyok.WebApi.Controllers
{
    /// <summary>
    /// ����Api
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BlogController : BaseController
    {
        private static LogWriter Logger = new LogWriter();
        private readonly IRepository<Article, int> _blogArticleRepository;

        private readonly IRepository<Tag, int> _blogTagRepository;

        private readonly IRepository<ArticleTag, int> _articleTagRepository;

        private readonly IBlogContract _blogService;

        /// <summary>
        /// ���캯�� ��ʼ��ע��
        /// </summary>
        /// <param name="blogArticleRepository"></param>
        /// <param name="blogTagRepository"></param>
        /// <param name="articleTagRepository"></param>
        /// <param name="blogService"></param>
        public BlogController(
            IRepository<Article, int> blogArticleRepository,
            IRepository<Tag, int> blogTagRepository,
            IRepository<ArticleTag, int> articleTagRepository,
            IBlogContract blogService)
        {
            this._blogArticleRepository = blogArticleRepository;
            this._blogTagRepository = blogTagRepository;
            this._articleTagRepository = articleTagRepository;
            this._blogService = blogService;
        }

        /// <summary>
        /// ��ȡ�����б���ҳ��
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Article/{pageIndex:int}/{pageSize:int}")]
        public ArticleModel GetArticleList(int pageIndex,int pageSize)
        {
            var list = Utility.CollectionExtensions.Where<Article, int>(_blogArticleRepository.Entities, m => m.Id > 0, pageIndex, pageSize, out int total);
            //var likeList=_blogArticleRepository.Entities.Where(item => EF.Functions.Like(item.Title, "a%"));    //ģ����ѯ��������Ӧ��
            var result = list.ToList().Select(item => new Article()
            {
                Id = item.Id,
                Title = item.Title,
                PostTime = item.PostTime,
                UpdateTime = item.UpdateTime,
                Count = item.Count
            }).ToList();

            Logger.Info("��ѯ�����б�");
            ArticleModel model = new ArticleModel()
            {
                ArticleList = result,
                Count = total
            };
            return model;
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Detail/{id:int}")]
        public Article GetArticle(int id)
        {
            var blog = _blogArticleRepository.Entities.FirstOrDefault(item => item.Id == id);
            return blog;
        }

        /// <summary>
        /// ��Ӳ���
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]    //����[HttpPost] Swagger���ʱ���
        [Route("Add/{id:int}")]
        public CreateArticle GetCreateArticle(int id = 0)
        {
            CreateArticle createArticle = new CreateArticle()
            {
                Article = new Article(),
                Tags = new Tags()
            };
            if (id > 0)
            {
                createArticle.Article = this._blogArticleRepository.Entities.FirstOrDefault(item => item.Id == id);

                var list = from tags in this._articleTagRepository.Entities.Where(item => item.ArticleId == id)
                           select tags.TagId;
                string tagList = string.Join(",", list.ToArray());
                createArticle.Tags.TagId = tagList;
            }
            return createArticle;
        }
        /// <summary>
        /// ��ȡ���·����б�
        /// </summary>
        /// <param name="pageIndex">�ڼ�ҳ</param>
        /// <param name="pageSize">һҳ������</param>
        /// <returns></returns>
        [HttpGet]
        [Route("TagList/{pageIndex:int}/{pageSize:int}")]
        public TagModel GetTagList(int pageIndex,int pageSize)
        {
            var list = Utility.CollectionExtensions.Where<Tag, int>(_blogTagRepository.Entities, m => m.Id > 0, pageIndex, pageSize, out int total);
            var result = list.ToList().Select(item => new Tag()
            {
                Id = item.Id,
                Name = item.Name,
                DisplayOrder = item.DisplayOrder
            }).ToList();
            TagModel model = new TagModel()
            {
                TagList = result,
                Count = total
            };
            return model;
        }

        /// <summary>
        /// ���ͱ�ǩ�б�
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TagList")]
        public dynamic GetTagList()
        {
            var list = _blogTagRepository.Entities.Select(item => new
            {
                id = item.Id,
                name = item.Name
            }).ToList();
            //var json = JsonConvert.SerializeObject(list);
            //return Ok(json);
            return list;
        }

        /// <summary>
        /// ��ȡ���ͱ�ǩ����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Tag/{Id:int}")]
        public Tag GetTag(int id = 0)
        {
            var tag = this._blogTagRepository.Entities.FirstOrDefault(item => item.Id == id);
            return tag;
        }

        [HttpPost]
        [Route("Tag")]
        public async Task SaveTag([FromBody]Tag tag)
        {
            await this._blogTagRepository.InsertAsync(tag);
        }

        /// <summary>
        /// ɾ�����ͱ�ǩ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Tag/{Id:int}")]
        public async Task DeleteTag(int id = 0)
        {
            await this._blogTagRepository.DeleteAsync(id);
        }

        /// <summary>
        /// ���沩������
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Route("SaveBlogArticle")]
        //http://www.cnblogs.com/babycool/p/3922738.html
        public async Task SaveBlogArticle([FromBody]CreateArticle article)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                article.Article.PostTime = dateTime;
                article.Article.UpdateTime = dateTime;
                if (article.Article.Id < 1)  //Add
                {
                    await this._blogService.InsertBlog(article.Article, article.Tags.TagId);
                }
                else  //Update
                {
                    Article blog = new Article();

                    blog = this._blogArticleRepository.Entities.FirstOrDefault(m => m.Id == article.Article.Id);
                    blog.Title = article.Article.Title;
                    blog.Content = article.Article.Content;
                    blog.PostTime = dateTime;
                    blog.UpdateTime = dateTime;
                    await this._blogService.UpdateBlog(blog, article.Tags.TagId);
                }
            }
            catch (Exception exception)
            {
                Logger.Error("�������ݱ���ʱ�����쳣", exception);
            }
        }

        /// <summary>
        /// ɾ����������
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Article/{id:int}")]
        public async Task DeleteBlogArticle(int id)
        {
            await _blogArticleRepository.DeleteAsync(id);
        }
    }
}