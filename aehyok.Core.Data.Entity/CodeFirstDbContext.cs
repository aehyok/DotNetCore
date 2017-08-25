using aehyok.Base;
using aehyok.Core.Data.Entity.Configurations.Blog;
using aehyok.Model.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            //http://gunnarpeipman.com/2017/08/ef-core-global-query-filters/
            //foreach (var type in _entityTypeProvider.GetEntityTypes())
            //{
            //    var method = SetGlobalQueryMethod.MakeGenericMethod(type);
            //    method.Invoke(this, new object[] { builder });
            //}
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(CodeFirstDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        public void SetGlobalQuery<T>(ModelBuilder builder) where T :EntityBase<object>
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override int SaveChanges()
        {
            if (TransactionEnabled)
            {
                return 0;
            }
            else
            {
                return base.SaveChanges();
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public async Task<int> SaveChangesAsync()
        {
            if(TransactionEnabled)
            {
                return 0;
            }
            else
            {
                return await base.SaveChangesAsync();
            }
        }

        //配置SqlServer数据库
        //程序包管理器控制台输入 Add-Migration ApiMigration会在生成相应的数据库
        //http://www.cnblogs.com/DaphneOdera/p/6573066.html  命令迁移
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=aehyokCore2;Persist Security Info=True;User ID=sa;Password=M9y2512;");
        }

        public async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess)
        {
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess);
        }

        public bool TransactionEnabled { get; set; }
    }
}
