using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
namespace aehyok.SignalR.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://aehyok.com:5004")
                .Build();
    }
}
