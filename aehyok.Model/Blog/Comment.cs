using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Base;

namespace aehyok.Model.Blog
{
    /// <summary>
    /// 博客——评论类
    /// </summary>
    public class Comment:EntityBase<int>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 评论上级Id
        /// </summary>
        public int FatherId { get; set; }

        public int ArticleId { get; set; }

        /// <summary>
        /// 博客文章
        /// </summary>
        public virtual Article Article { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime PostTime { get; set; } 
    }
}
