using Discord.Interactions;

namespace BillyBosta_DiscordApp.Modules;

public class InteractionModule : InteractionModuleBase<ShardedInteractionContext>
{
    [SlashCommand("echo", "Echo an input")]
    public async Task Echo(string input)
    {
        await RespondAsync(input);
    }
}