using aehyok.Model.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.WebApi.ViewModel
{
    /// <summary>
    /// 标签ViewModel
    /// </summary>
    public class TagModel
    {
        /// <summary>
        /// 标签列表
        /// </summary>
        public List<Tag> TagList { get; set; }

        /// <summary>
        /// 标签数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 构造函数初始化
        /// </summary>
        public TagModel()
        {
            Count = 0;
        }
    }
}
