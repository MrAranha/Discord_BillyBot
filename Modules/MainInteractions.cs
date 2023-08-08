using BillyBosta_DiscordApp.Handlers;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillyBosta_DiscordApp.Modules
{
    // Interaction modules must be public and inherit from an IInteractionModuleBase
    public class MainInteractions : InteractionModuleBase<SocketInteractionContext>
    {
        public InteractionService Commands { get; set; }
        private InteractionHandler _handler;

        public MainInteractions(InteractionHandler handler)
        {
            _handler = handler;
        }

        [SlashCommand("ping", "Pings the bot and returns its latency.")]
        public async Task GreetUserAsync()
            => await RespondAsync(text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!", ephemeral: true);

    }
}
