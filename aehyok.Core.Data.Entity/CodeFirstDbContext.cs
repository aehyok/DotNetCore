using aehyok.Base;
using aehyok.Core.Data.Entity.Configurations.Blog;
using aehyok.Model.Blog;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
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
    public class CodeFirstDbContext : IdentityDbContext<IdentityUser,IdentityRole,string>, IUnitOfWork, IDependency
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

            foreach (var type in GetEntityTypes())
            {

                var method = SetGlobalQueryMethod.MakeGenericMethod(type);
                method.Invoke(this, new object[] { builder });
            }
        }

        private static IList<Type> _entityTypeCache;
        private static IList<Type> GetEntityTypes()
        {
            if (_entityTypeCache != null)
            {
                return _entityTypeCache.ToList();
            }

            _entityTypeCache = (from a in GetReferencingAssemblies()
                                from t in a.DefinedTypes
                                where t.BaseType == typeof(EntityBase<int>)
                                select t.AsType()).ToList();

            return _entityTypeCache;
        }

        /// <summary>
        /// 获取当前引用的dll文件列表
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Assembly> GetReferencingAssemblies()
        {
            var assemblies = new List<Assembly>();
            var dependencies = DependencyContext.Default.RuntimeLibraries;

            foreach (var library in dependencies)
            {
                try
                {
                    if(library.Name.Contains("aehyok"))
                    {
                        var assembly = Assembly.Load(new AssemblyName(library.Name));
                        assemblies.Add(assembly);
                    }
                }
                catch (FileNotFoundException)
                { }
            }
            return assemblies;
        }

        static readonly MethodInfo SetGlobalQueryMethod = typeof(CodeFirstDbContext).GetMethods(BindingFlags.Public | BindingFlags.Instance)
                                                        .Single(t => t.IsGenericMethod && t.Name == "SetGlobalQuery");

        /// <summary>
        /// 设置全局查询过滤
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        public void SetGlobalQuery<T>(ModelBuilder builder) where T :EntityBase<int>
        {
            builder.Entity<T>().HasKey(e => e.Id);
            builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
        }

        //public override int SaveChanges()
        //{
        //    if (TransactionEnabled)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return base.SaveChanges();
        //    }
        //}

        //public override int SaveChanges(bool acceptAllChangesOnSuccess)
        //{
        //    return base.SaveChanges(acceptAllChangesOnSuccess);
        //}

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
