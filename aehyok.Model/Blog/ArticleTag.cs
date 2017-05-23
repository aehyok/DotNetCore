using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aehyok.Base;

namespace aehyok.Model.Blog
{
    public class ArticleTag:EntityBase<int>
    {
        public int ArticleId { get; set; }

        public Article Article { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
