using aehyok.Model.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aehyok.WebApi.ViewModel
{
    public class TagModel
    {
        public List<Tag> TagList { get; set; }

        public int Count { get; set; }


        public TagModel()
        {
            Count = 0;
        }
    }
}
