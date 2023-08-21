using BillyBosta_DiscordApp.Notifications;
using MediatR;
using Discord;

namespace BillyBosta_DiscordApp.Handlers;

public class MessageReceivedHandler : INotificationHandler<MessageReceivedNotification>
{
    public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
    {
        /*if (notification.Message.Content.Contains("teste"))
        {
            DeleteMessageAsync(notification.Message.Content);
        }*/
        Console.WriteLine($"MediatR works! (Received a message by {notification.Message.Author.Username})");
        // Your implementation
    }
}