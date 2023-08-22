using BillyBosta_DiscordApp.DTOs;
using BillyBosta_DiscordApp.Handlers;
using BillyBosta_DiscordApp.Interfaces;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System.Text.Json;

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
            .AddSingleton<CommandService>()
            .AddSingleton<IMessageErrorsHandler, MessageErrorsHandler>()
            .AddSingleton<CommandHandlingService>()
            .AddSingleton<HttpClient>()
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

        // Here we initialize the logic required to register our commands.

        using StreamReader reader = new("appsettings.json");
        var json = reader.ReadToEnd();
        AppSettingsDTO AppSettings = JsonConvert.DeserializeObject<AppSettingsDTO>(json);

        await client.LoginAsync(TokenType.Bot, AppSettings.Token);
        await client.StartAsync();

        await services.GetRequiredService<CommandHandlingService>().InitializeAsync();
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