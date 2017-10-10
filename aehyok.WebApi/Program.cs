using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace aehyok.WebApi
{
    /// <summary>
    /// 程序启动类
    /// </summary>
    public class Program
    {
        ///// <summary>
        ///// 入口函数
        ///// </summary>
        ///// <param name="args"></param>
        //public static void Main(string[] args)
        //{
        //    var host = new WebHostBuilder()
        //        //.UseUrls("http://localhost:6666")
        //        .UseKestrel()
        //        .UseContentRoot(Directory.GetCurrentDirectory())
        //        .UseIISIntegration()
        //        .UseStartup<Startup>()
        //        .UseApplicationInsights()
        //        .Build();
        //    host.Run();
        //}

        public static void Main(string[] args)
        {
            Console.Title = "API";

            BuildWebHost(args).Run();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
