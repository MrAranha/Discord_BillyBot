using MediatR;

namespace BillyBosta_DiscordApp.Notifications;

public class ReadyNotifications : INotification
{
    public static readonly ReadyNotifications Default
        = new();

    private ReadyNotifications()
    {
    }
}