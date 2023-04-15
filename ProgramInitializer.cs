using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Discord.Addons.Hosting.Util;
using Microsoft.Extensions.DependencyInjection;

namespace Main
{
    class ProgramInitializer
    {
        static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
            .ConfigureDiscordHost((context, config) =>
            {
                config.SocketConfig = new DiscordSocketConfig
                {
                    LogLevel = LogSeverity.Verbose,
                    AlwaysDownloadUsers = true,
                    MessageCacheSize = 200
                };

                config.Token = context.Configuration["token"];
            })
            // Optionally wire up the command service
            .UseCommandService((context, config) =>
            {
                config.DefaultRunMode = RunMode.Async;
                config.CaseSensitiveCommands = false;
            })
            // Optionally wire up the interactions service
            .UseInteractionService((context, config) =>
            {
                config.LogLevel = LogSeverity.Info;
                config.UseCompiledLambda = true;
            })
            .ConfigureServices((context, services) =>
            {
                //Add any other services here
                services.AddHostedService<CommandHandler>();
                services.AddHostedService<BotStatusService>();
                services.AddHostedService<LongRunningService>();
            }).Build();

        }

        public async Task MainAsync()
        {
        }

        private Task Log(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }
    }

}