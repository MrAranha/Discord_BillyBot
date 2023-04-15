﻿using System.Reflection;
using Discord;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;


namespace BillyBosta_DiscordApp
{
    public class CommandHandler : DiscordShardedClientService
    {
        private readonly IServiceProvider _provider;
        private readonly CommandService _commandService;
        private readonly IConfiguration _config;

        public CommandHandler(DiscordShardedClient client, ILogger<CommandHandler> logger, IServiceProvider provider, CommandService commandService, IConfiguration config) : base(client, logger)
        {
            _provider = provider;
            _commandService = commandService;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Client.MessageReceived += HandleMessage;
            _commandService.CommandExecuted += CommandExecutedAsync;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task HandleMessage(SocketMessage incomingMessage)
        {
            if (incomingMessage is not SocketUserMessage message) return;
            if (message.Source != MessageSource.User) return;

            int argPos = 0;
            if (!message.HasStringPrefix(_config["Prefix"], ref argPos) && !message.HasMentionPrefix(Client.CurrentUser, ref argPos)) return;

            var context = new ShardedCommandContext(Client, message);

            await _commandService.ExecuteAsync(context, argPos, _provider);
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            Logger.LogInformation("Usuário {user} tentou usar o comando {command}", context.User, command.Value.Name);

            if(!command.IsSpecified || result.IsSuccess)
            {
                return;
            }

            await context.Channel.SendMessageAsync($"Error: {result}");
        }
    }
}