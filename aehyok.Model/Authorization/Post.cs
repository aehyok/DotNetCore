using System.ComponentModel.DataAnnotations;
using aehyok.Base;

namespace aehyok.Model.Authorization
{
    /// <summary>
    /// 岗位实体类
    /// </summary>
    public class Post : EntityBase<int>
    {
        /// <summary>
        /// 岗位名称
        /// </summary>
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 岗位描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 展示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        public int OrganizeId { get; set; }

        public virtual Organize Organize{ get; set; }
    }
}
