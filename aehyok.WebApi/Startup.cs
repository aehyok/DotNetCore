using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using aehyok.Core.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using aehyok.Core.Data;
using aehyok.Model.Blog;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using aehyok.Core;
using System.Reflection;
using System.Runtime.Loader;

namespace aehyok.WebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        public IConfigurationRoot Configuration { get; }



        // 需要先删除void类型的ConfigureServices方法
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //http://www.cnblogs.com/TomXu/p/4496440.html
            services.AddScoped<CodeFirstDbContext>();
            //services.AddTransient(typeof(IRepository<Tag, int>), typeof(Repository<Tag, int>));

            // Add framework services.
            services.AddMvc();

            //Web Api Swagger插件的引入
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Swashbuckle.Swagger.Model.Info
                {
                    Version = "v1",
                    Title = "aehyok",
                    Description = "aehyok.com Web Api Interface Manage",
                    TermsOfService = "None"
                });
                options.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    "aehyok.WebApi.xml")); // 注意：此处替换成所生成的XML documentation的文件名。
                options.DescribeAllEnumsAsStrings();


                //注册Identity
                services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<CodeFirstDbContext>()
                .AddDefaultTokenProviders();
            });

            services.AddDbContext<CodeFirstDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));  //设置数据库链接字符串

            var builder = new ContainerBuilder();  // 构造容器构建类
            builder.Populate(services);  //将现有的Services路由到Autofac的管理集合中

            builder.RegisterModule(new AutofacModule());
            IContainer container = builder.Build();

            return container.Resolve<IServiceProvider>(); //返回AutoFac实现的IServiceProvider
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{
           
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
            //app.Filters.Add(new WebApiExceptionFilterAttribute());


            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<CodeFirstDbContext>();
                bool HasCreated = dbContext.Database.EnsureCreated();
                if (HasCreated)
                {
                    //MuscleFellowSampleDataInitializer dbInitializer = new MuscleFellowSampleDataInitializer(dbContext);
                    //dbInitializer.LoadBasicInformationAsync().Wait();
                    //dbInitializer.LoadSampleDataAsync().Wait();
                }
            }
        }
    }

    public class AutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var baseType = typeof(IDependency);
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));
            var url = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var fileNames = Directory.GetFiles(url, "*.dll");

            var assemblyNames = fileNames
        .Select(AssemblyLoadContext.GetAssemblyName);

            List<Assembly> assemblies = new List<Assembly>();
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }
            //this.Resolver.GetRequiredService<ILibraryManager>();

            //var assenblys=AssemblyLoadContext.Default.LoadFromAssemblyPath(url);
            //var assemblys = Assembly.GetEntryAssembly(); //AppDomain.CurrentDomain.GetReferencedAssemblies().OfType<Assembly>().ToList<Assembly>(); //AppDomain.CurrentDomain.GetAssemblies().ToList();
            //builder.RegisterControllers(assemblys.ToArray());

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces().InstancePerLifetimeScope();
            //var container = builder.Build();
            //var configuration = GlobalConfiguration.Configuration;
            //configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
