using Discord.Bot.Shared.Models;

namespace Discord.Bot.Shared.Events;

public class DiscordMessageNotificationEvent
{
    public SourceMessageInfo SourceMessageInfo { get; set; } = null!;
    public string NotificationMessage { get; set; } = null!;
}