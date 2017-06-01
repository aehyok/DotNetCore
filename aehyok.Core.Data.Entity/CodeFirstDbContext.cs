using aehyok.Core.Data.Entity.Configurations.Blog;
using aehyok.Model.Blog;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Data.Entity
{
    /// <summary>
    /// EntityFramework-CodeFirst数据上下文
    /// </summary>
    public class CodeFirstDbContext : IdentityDbContext<IdentityUser>, IUnitOfWork, IDependency
    {
        //public DbSet<Tag> Tag { get; set; }
        //public DbSet<ArticleTag> ArticleTag { get; set; }
        //public DbSet<Article> Article { get; set; }
        //public DbSet<Comment> Comment { get; set; }
        public bool TransactionEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CodeFirstDbContext(DbContextOptions options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //https://github.com/aspnet/EntityFramework/issues/2805
            builder.AddEntityConfigurationsFromAssembly(GetType().GetTypeInfo().Assembly);
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
