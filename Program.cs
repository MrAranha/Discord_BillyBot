using BillyBosta_DiscordApp;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;

IHost host = Host.CreateDefaultBuilder()
    .ConfigureDiscordShardedHost((context, config) =>
    {
        config.SocketConfig = new DiscordSocketConfig
        {
            LogLevel = LogSeverity.Verbose,
            AlwaysDownloadUsers = true,
            MessageCacheSize = 200,
            TotalShards = 4
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
        services.AddHostedService<InteractionHandler>();
    }).Build();

await host.RunAsync();
