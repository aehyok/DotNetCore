using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.Dapper.Model
{
    public class MD_GuideLineFieldName
    {
        /// <summary>
        /// 本字段是否可隐藏
        /// </summary>
        public bool CanHide { get; set; }

        /// <summary>
        /// 显示格式
        /// </summary>
        public string DisplayFormat { get; set; }

        /// <summary>
        /// 字段名称（大写）
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段名称（原生）
        /// </summary>
        public string OriginalFieldName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        /// 
        public string DisplayTitle { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        /// 
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 显示宽度
        /// </summary>
        /// 
        public int DisplayWidth { get; set; }

        /// <summary>
        /// 是否居中
        /// </summary>
        /// 
        public string TextAlign { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public string GroupName { get; set; }

        public MD_GuideLineFieldName()
        {
        }

        public MD_GuideLineFieldName(string fname, string title, int order, int width, string align, string format, bool hide, string groupName)
        {
            FieldName = fname;
            DisplayTitle = title;
            DisplayOrder = order;
            DisplayWidth = width;
            TextAlign = align;
            DisplayFormat = format;
            CanHide = hide;
            GroupName = groupName;
        }

        public MD_GuideLineFieldName(string fname, string title, int order, int width, string align, string format, bool hide)
        {
            FieldName = fname;
            DisplayTitle = title;
            DisplayOrder = order;
            DisplayWidth = width;
            TextAlign = align;
            DisplayFormat = format;
            CanHide = hide;
        }

        public MD_GuideLineFieldName(string orifname, string fname, string title, int order, int width, string align, string format, bool hide)
        {
            OriginalFieldName = orifname;
            FieldName = fname;
            DisplayTitle = title;
            DisplayOrder = order;
            DisplayWidth = width;
            TextAlign = align;
            DisplayFormat = format;
            CanHide = hide;
        }
    }
}
