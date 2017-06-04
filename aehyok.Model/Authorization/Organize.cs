using aehyok.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Model.Authorization
{
    /// <summary>
    /// 组织机构实体类
    /// </summary>
    public class Organize:EntityBase<int>
    {
        /// <summary>
        /// 组织结构名称
        /// </summary>
        [MaxLength(20)]
        public string Name { get; set; }

        /// <summary>
        /// 组织机构全称
        /// </summary>
        [MaxLength(100)]
        public string FullName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 上级Id
        /// </summary>
        public int FatherId { get; set; }

        public virtual ICollection<Post> PostList { get; set; }
    }
}
