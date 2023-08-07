using BillyBosta_DiscordApp;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace BillyBosta_DiscordApp;

public class Program
{
    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            .AddMediatR(typeof(Program))
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.AllUnprivileged,
                LogLevel = LogSeverity.Info
            }))
            .AddSingleton<DiscordEventListener>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .BuildServiceProvider();
    }

    public static async Task Main()
    {
        await new Program().RunAsync();
    }

    private async Task RunAsync()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        await using var services = ConfigureServices();

        var client = services.GetRequiredService<DiscordSocketClient>();
        client.Log += LogAsync;

        var listener = services.GetRequiredService<DiscordEventListener>();
        await listener.StartAsync();

        await client.LoginAsync(TokenType.Bot, "MTA5NjYwMTk3NTY2OTQwNzg0NA.GfTwrO.RtqTI-orVZ1SoCXOpQcfNoypGWGytKTd3O9VYI");
        await client.StartAsync();

        await Task.Delay(Timeout.Infinite);
    }

    private static Task LogAsync(LogMessage message)
    {
        var severity = message.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };

        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);

        return Task.CompletedTask;
    }
    public static bool IsDebug()
    {
        #if DEBUG
                return true;
        #else
                    return false;
        #endif
    }
}