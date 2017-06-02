using aehyok.Contracts;
using aehyok.Core;
using aehyok.Core.Data;
using aehyok.Model.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Services
{
    public class BlogService : ServiceBase, IBlogContract
    {
        private readonly IRepository<Article, int> _articleRepository;

        private readonly IRepository<Tag, int> _tagRepository;

        private readonly IRepository<ArticleTag, int> _articleTagRepository;
        public BlogService(IRepository<Article, int> articleRepository,
            IRepository<Tag, int> tagRepository,
            IRepository<ArticleTag, int> articleTagRepository)
            : base(articleRepository.UnitOfWork)
        {
            this._articleRepository = articleRepository;
            this._tagRepository = tagRepository;
            this._articleTagRepository = articleTagRepository;
        }

        public async Task<int> UpdateBlog(Article article, string tags)
        {
            this.UnitOfWork.TransactionEnabled = true;
            await this._articleRepository.UpdateAsync(article);
            await this._articleTagRepository.DeleteAsync(item => item.ArticleId == article.Id);
            foreach (var item in tags.Split(','))
            {
                ArticleTag articleTag = new ArticleTag()
                {
                    ArticleId = article.Id,
                    TagId = int.Parse(item)
                };
                await this._articleTagRepository.InsertAsync(articleTag);
            }
            return this.UnitOfWork.SaveChanges();
        }


        public async Task<int> InsertBlog(Article article, string tags)
        {
            this.UnitOfWork.TransactionEnabled = true;
            await this._articleRepository.InsertAsync(article);
            foreach (var item in tags.Split(','))
            {
                ArticleTag articleTag = new ArticleTag()
                {
                    ArticleId = article.Id,
                    TagId = int.Parse(item)
                };
                await this._articleTagRepository.InsertAsync(articleTag);
            }
            return this.UnitOfWork.SaveChanges();
        }
    }
}
