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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<CodeFirstDbContext>();
            services.AddTransient(typeof(IRepository<Tag,int>), typeof(Repository<Tag,int>));

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
            });

            services.AddDbContext<CodeFirstDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));  //设置数据库链接字符串

            //注册Identity
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<CodeFirstDbContext>()
            .AddDefaultTokenProviders();

        }

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
}
