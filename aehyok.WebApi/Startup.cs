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
    /// <summary>
    /// 初始化类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 启动方法
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

        }

        /// <summary>
        /// 配置根
        /// </summary>
        public IConfigurationRoot Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        //public void ConfigureServices(IServiceCollection services)
        //{

        //}

        /// <summary>
        /// 需要先删除void类型的ConfigureServices方法
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CodeFirstDbContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));  //设置数据库链接字符串


            //http://www.cnblogs.com/TomXu/p/4496440.html
            //注册Identity
            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Cookies.ApplicationCookie.AuthenticationScheme = "ApplicationCookie";
                options.Cookies.ApplicationCookie.CookieName = "Interop";
            })
            .AddEntityFrameworkStores<CodeFirstDbContext>()
            .AddDefaultTokenProviders();

            // Add framework services.
            services.AddMvc();

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            AddSwagger(services);



            var builder = new ContainerBuilder();  // 构造容器构建类
            builder.Populate(services);  //将现有的Services路由到Autofac的管理集合中

            builder.RegisterModule(new AutofacModule());
            IContainer container = builder.Build();

            return container.Resolve<IServiceProvider>(); //返回AutoFac实现的IServiceProvider
        }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            // 使用了 CookieAuthentication 中间件做身份认证
            app.UseIdentity();

            app.UseSwagger();
            app.UseSwaggerUi();

            // this uses the policy called "default"
            app.UseCors("default");

            app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            {
                Authority = "http://localhost:5000",
                RequireHttpsMetadata = false,

                ApiName = "api1"
            });

            app.UseMvc();
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<CodeFirstDbContext>();
                bool HasCreated = dbContext.Database.EnsureCreated();
                if (HasCreated)   //数据库是否被创建(如果数据库第一次被创建，可进行初始化默认数据)
                {
                    //MuscleFellowSampleDataInitializer dbInitializer = new MuscleFellowSampleDataInitializer(dbContext);
                    //dbInitializer.LoadBasicInformationAsync().Wait();
                    //dbInitializer.LoadSampleDataAsync().Wait();
                }
            }
        }

        #region 自定义扩展


        /// <summary>
        /// 注册引入Swagger插件
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(IServiceCollection services)
        {
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
            });
        }
        #endregion
    }

    /// <summary>
    /// Autofac IOC
    /// </summary>
    public class AutofacModule : Autofac.Module
    {
        /// <summary>
        /// 重写自动加载方法
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            var baseType = typeof(IDependency);
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));
            var url = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var fileNames = Directory.GetFiles(url, "*.dll");

            var assemblyNames = fileNames.Select(AssemblyLoadContext.GetAssemblyName);

            List<Assembly> assemblies = new List<Assembly>();
            foreach (AssemblyName assemblyName in assemblyNames)
            {
                assemblies.Add(Assembly.Load(assemblyName));
            }

            builder.RegisterAssemblyTypes(assemblies.ToArray())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
