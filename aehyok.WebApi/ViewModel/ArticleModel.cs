using aehyok.Model.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.WebApi.ViewModel
{
    /// <summary>
    /// 博客文档ViewModel
    /// </summary>
    public class ArticleModel
    {
        /// <summary>
        /// 文章List
        /// </summary>
        public List<Article> ArticleList { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public ArticleModel()
        {
            Count = 0;
        }
    }
}
