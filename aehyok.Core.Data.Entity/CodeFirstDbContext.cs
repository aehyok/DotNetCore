using aehyok.Core.Data.Entity.Configurations.Blog;
using aehyok.Model.Blog;
using Microsoft.AspNetCore.Identity;
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

        public CodeFirstDbContext()
        {
            
        }
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
            return base.SaveChangesAsync();
        }

        public bool TransactionEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //配置SqlServer数据库
        //程序包管理器控制台输入 Add-Migration ApiMigration会在生成相应的数据库
        //http://www.cnblogs.com/DaphneOdera/p/6573066.html  命令迁移
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=aehyokCore2;Persist Security Info=True;User ID=sa;Password=M9y2512;");
        }
    }
}
