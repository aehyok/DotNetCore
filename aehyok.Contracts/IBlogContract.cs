using aehyok.Core;
using aehyok.Model.Blog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Contracts
{
    public interface IBlogContract: IDependency
    {
        Task<int> UpdateBlog(Article article, string tags);

        Task<int> InsertBlog(Article article, string tags);
    }
}
