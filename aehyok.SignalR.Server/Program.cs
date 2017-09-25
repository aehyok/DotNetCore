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
                .UseUrls("http://192.168.1.80:5004")
                .Build();
    }
}
