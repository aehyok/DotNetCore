using System.ComponentModel.DataAnnotations;
using aehyok.Base;

namespace aehyok.Model.Authorization
{
    /// <summary>
    /// 菜单实体类
    /// </summary>
    public class Menu:EntityBase<int>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        [MaxLength(20)]
        public string Title { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        public int FatherId { get; set; }

        /// <summary>
        ///参数列表 
        /// </summary>
        public string MetaParameter { get; set; }

        /// <summary>
        /// 展示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 菜单控制器
        /// </summary>
        [MaxLength(40)]
        public string Controller { get; set; }

        /// <summary>
        /// 菜单Action
        /// </summary>
        [MaxLength(40)]
        public string Action { get; set; }
    }
}
