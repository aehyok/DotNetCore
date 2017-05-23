using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Base;

namespace aehyok.Model.Blog
{
    /// <summary>
    /// 博客——文章
    /// </summary>
    public class Article:EntityBase<int>
    {
        /// <summary>
        /// 博客标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 博客内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime PostTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 博客评论列表
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
