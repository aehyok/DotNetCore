using aehyok.Model.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.WebApi.ViewModel
{
    /// <summary>
    /// 创建文章
    /// </summary>
    public class CreateArticle
    {
        /// <summary>
        /// 文章
        /// </summary>
        public Article Article { get; set; }

        /// <summary>
        /// 博文标签列表
        /// </summary>
        public List<Tag> SelectList { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public Tags Tags { get; set; }
    }

    /// <summary>
    /// 标签Ids
    /// </summary>
    public class Tags
    {
        /// <summary>
        /// 标签Id
        /// </summary>
        public string TagId { get; set; }
    }
}
