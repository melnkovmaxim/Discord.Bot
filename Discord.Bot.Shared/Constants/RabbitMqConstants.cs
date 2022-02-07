namespace Discord.Bot.Shared.Constants;

public class RabbitMqConstants
{
    public const string RabbitMqUri = "other.rabbitmq";
    public const string UserName = "guest";
    public const string Password = "guest";
    public const string DiscordMessageCommandTextQueue = "handle.discord.message.command.text";
    public const string DiscordMessageCommandMediaQueue = "handle.discord.message.command.media";
    public const string DiscordMessageCommandNotificationQueue = "handle.discord.message.event.notification";
}