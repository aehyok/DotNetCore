using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Base;

namespace aehyok.Model.Blog
{
    public class Tag:EntityBase<int>
    {
        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int DisplayOrder { get; set; }

    }
}
