using Discord.Bot.Shared.Events;
using Discord.WebSocket;
using MassTransit;

namespace Discord.Bot.Notification;

public class DiscordMessageNotificationEventConsumer: IConsumer<DiscordMessageNotificationEvent>
{
    private readonly DiscordSocketClient _discordClient;

    public DiscordMessageNotificationEventConsumer(DiscordSocketClient discordSocketClient)
    {
        _discordClient = discordSocketClient;
    }
    
    public async Task Consume(ConsumeContext<DiscordMessageNotificationEvent> context)
    {
        var message = context.Message;
        var channel = await _discordClient.GetChannelAsync(message.SourceMessageInfo.ChannelId);
        var socketChannel = channel as ISocketMessageChannel;

        await socketChannel!.SendMessageAsync(message.NotificationMessage);
    }
}