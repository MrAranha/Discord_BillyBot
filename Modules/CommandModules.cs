using Discord.Commands;
using Discord.WebSocket;

namespace BillyBosta_DiscordApp.Modules;

public class CommandModules : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Alias("pong", "hello")]
    public Task Pong() =>
        ReplyAsync("Pong!");
    
}