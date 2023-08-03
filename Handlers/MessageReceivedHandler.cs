using BillyBosta_DiscordApp.Notifications;
using MediatR;

namespace BillyBosta_DiscordApp.Handlers;

public class MessageReceivedHandler : INotificationHandler<MessageReceivedNotification>
{
    public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"MediatR works! (Received a message by {notification.Message.Author.Username})");
        // Your implementation
    }
}