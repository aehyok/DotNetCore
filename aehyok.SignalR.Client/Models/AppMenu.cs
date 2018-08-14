using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace aehyok.Users.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AppMenu
    {
        public string Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [DataMember]
        [MaxLength(20)]
        public string Title { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// 上级菜单Id
        /// </summary>
        [DataMember]
        public int FatherId { get; set; }

        /// <summary>
        ///参数列表 
        /// </summary>
        [DataMember]
        public string MetaParameter { get; set; }

        /// <summary>
        /// 展示顺序
        /// </summary>
        [DataMember]
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 菜单控制器
        /// </summary>
        [MaxLength(40)]
        [DataMember]
        public string Controller { get; set; }

        /// <summary>
        /// 菜单Action
        /// </summary>
        [DataMember]
        [MaxLength(40)]
        public string Action { get; set; }

        public virtual ICollection<AppRole> Roles { get; set; }
    }
}