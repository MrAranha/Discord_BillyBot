using System.Reflection;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using BillyBosta_DiscordApp;
using Discord.Addons.Hosting.Util;

namespace BillyBosta_DiscordApp
{
    public class BotStatusService : DiscordShardedClientService
    {
        public BotStatusService(DiscordShardedClient client, ILogger<BotStatusService> logger) : base(client, logger) 
        { 
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Client.WaitForReadyAsync(stoppingToken);
            Logger.LogInformation("BOT RODANDO");

            await Client.SetActivityAsync(new Game("Billy Botsa"));

        }
    }
}
