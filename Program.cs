using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using StravaDiscordBot.Extensions;

namespace StravaDiscordBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
