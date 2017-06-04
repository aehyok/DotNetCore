using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using aehyok.Base;

namespace aehyok.Model.Authorization
{
    /// <summary>
    /// 角色——实体类 added by aehyok
    /// </summary>
    public class Role:EntityBase<int>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [MaxLength(40)]
        public string Name { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// 展示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 角色是否被选中(不映射到数据库)
        /// </summary>
        [NotMapped]
        public bool IsChecked { get; set; }
    }
}
