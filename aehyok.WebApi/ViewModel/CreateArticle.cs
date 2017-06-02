using aehyok.Model.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.WebApi.ViewModel
{
    public class CreateArticle
    {
        public Article Article { get; set; }

        public Tags Tags { get; set; }
    }

    public class Tags
    {
        public string TagId { get; set; }
    }
}
