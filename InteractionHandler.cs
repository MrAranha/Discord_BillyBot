﻿using System.Reflection;
using Discord;
using Discord.Addons.Hosting;
using Discord.Addons.Hosting.Util;
using Discord.Interactions;
using Discord.WebSocket;

namespace BillyBosta_DiscordApp
{
    internal class InteractionHandler : DiscordShardedClientService
    {
        private readonly IServiceProvider _provider;
        private readonly InteractionService _interactionService;
        private readonly IHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public InteractionHandler(DiscordShardedClient client, ILogger<DiscordShardedClientService> logger, IServiceProvider provider, InteractionService interactionService, IHostEnvironment environment, IConfiguration configuration) : base(client, logger)
        {
            _provider = provider;
            _interactionService = interactionService;
            _configuration = configuration;
            _environment = environment;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Process the InteractionCreated payloads to execute Interactions commands
            Client.InteractionCreated += HandleInteraction;

            // Process the command execution results 
            _interactionService.SlashCommandExecuted += SlashCommandExecuted;
            _interactionService.ContextCommandExecuted += ContextCommandExecuted;
            _interactionService.ComponentCommandExecuted += ComponentCommandExecuted;

            await _interactionService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
            await Client.WaitForReadyAsync(stoppingToken);

            // If DOTNET_ENVIRONMENT is set to development, only register the commands to a single guild
            /*if (_environment.IsDevelopment())
                await _interactionService.RegisterCommandsToGuildAsync(_configuration.GetValue<ulong>("DevGuild"));
            else*/
                await _interactionService.RegisterCommandsGloballyAsync();
        }

        private Task ComponentCommandExecuted(ComponentCommandInfo commandInfo, IInteractionContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // implement
                        break;
                    case InteractionCommandError.BadArgs:
                        // implement
                        break;
                    case InteractionCommandError.Exception:
                        // implement
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // implement
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task ContextCommandExecuted(ContextCommandInfo context, IInteractionContext arg2, IResult result)
        {
            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // implement
                        break;
                    case InteractionCommandError.BadArgs:
                        // implement
                        break;
                    case InteractionCommandError.Exception:
                        // implement
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // implement
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private Task SlashCommandExecuted(SlashCommandInfo commandInfo, IInteractionContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                switch (result.Error)
                {
                    case InteractionCommandError.UnmetPrecondition:
                        // implement
                        break;
                    case InteractionCommandError.UnknownCommand:
                        // implement
                        break;
                    case InteractionCommandError.BadArgs:
                        // implement
                        break;
                    case InteractionCommandError.Exception:
                        // implement
                        break;
                    case InteractionCommandError.Unsuccessful:
                        // implement
                        break;
                    default:
                        break;
                }
            }

            return Task.CompletedTask;
        }

        private async Task HandleInteraction(SocketInteraction arg)
        {
            try
            {
                // Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules
                var ctx = new ShardedInteractionContext(Client, arg);
                await _interactionService.ExecuteCommandAsync(ctx, _provider);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Exception occurred whilst attempting to handle interaction.");

                if (arg.Type == InteractionType.ApplicationCommand)
                {
                    var msg = await arg.GetOriginalResponseAsync();
                    await msg.DeleteAsync();
                }

            }
        }
    }
}
