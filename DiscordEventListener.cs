using Discord.WebSocket;
using MediatR;
using BillyBosta_DiscordApp.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace BillyBosta_DiscordApp
{
    public class DiscordEventListener
    {
        private readonly CancellationToken _cancellationToken;

        private readonly DiscordSocketClient _client;
        private readonly IServiceScopeFactory _serviceScope;
        public DiscordEventListener(CancellationToken cancellationToken, DiscordSocketClient client, IServiceScopeFactory serviceScope)
        {
            _cancellationToken = cancellationToken;
            _client = client;
            _serviceScope = serviceScope;
        }
        private IMediator Mediator
        {
            get
            {
                var scope = _serviceScope.CreateScope();
                return scope.ServiceProvider.GetRequiredService<IMediator>();
            }
        }

        public Task StartAsync()
        {
            _client.Ready += OnReadyAsync;
            _client.MessageReceived += OnMessageReceivedAsync;

            return Task.CompletedTask;
        }

        private Task OnMessageReceivedAsync(SocketMessage arg)
        {
            return Mediator.Publish(new MessageReceivedNotification(arg), _cancellationToken);
        }
    
        private Task OnReadyAsync()
        {
            return Mediator.Publish(ReadyNotifications.Default, _cancellationToken);
        }
    }
}
