#pragma warning disable 1591
#pragma warning disable SA1600
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication1
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
                .Build();
    }
}
#pragma warning restore SA1600
#pragma warning restore 1591