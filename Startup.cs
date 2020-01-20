using System;
using System.Net.Http;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StravaDiscordBot.Models;
using StravaDiscordBot.Services;
using StravaDiscordBot.Services.Discord;
using StravaDiscordBot.Services.Discord.Commands;
using StravaDiscordBot.Services.Storage;

namespace StravaDiscordBot
{
    public class Startup
    {
        private IConfiguration Configuration { get; set; }
        private DiscordSocketClient DiscordClient { get; set; }
        private CommandHandlingService CommandHandlingService { get; set; }

        public Startup(IConfiguration config)
        {
            Configuration = config;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var appOptions = new AppOptions();
            Configuration.Bind(appOptions);
            services.AddSingleton(appOptions);
            services.AddLogging();

            services.AddSingleton<IRemoteItService, RemoteItService>();

            services.AddSingleton<DiscordSocketClient>();
            services.AddSingleton<CommandService>();
            services.AddSingleton<CommandHandlingService>();
            services.AddSingleton<HttpClient>();

            services.AddSingleton<IRepository<LeaderboardParticipant>, LeaderboardParticipantRepository>();
            services.AddSingleton<IStravaService, StravaService>();
            services.AddSingleton<ICommand, JoinLeaderboardCommand>();
            services.AddSingleton<ICommand, ShowLeaderboardCommand>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            StartDiscordBot(app)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        private async Task StartDiscordBot(IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<AppOptions>();
            DiscordClient = app.ApplicationServices.GetRequiredService<DiscordSocketClient>();
            DiscordClient.Log += LogAsync;
            app.ApplicationServices.GetRequiredService<CommandService>().Log += LogAsync;
            await DiscordClient.LoginAsync(TokenType.Bot, options.Discord.Token);
            await DiscordClient.StartAsync();

            CommandHandlingService = app.ApplicationServices.GetService<CommandHandlingService>();
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());

            return Task.CompletedTask;
        }

    }
}
