using Discord.Bot.Shared.Commands;
using Discord.Bot.Shared.Enums;
using Discord.Bot.Shared.Events;
using MassTransit;

namespace Discord.Bot.TextCommandHandling;

public class DiscordMessageProcessingCommandConsumer: IConsumer<DiscordMessageProcessingCommand>
{
    private readonly IBusControl _bus;

    public DiscordMessageProcessingCommandConsumer(IBusControl bus)
    {
        _bus = bus;
    }
    
    public async Task Consume(ConsumeContext<DiscordMessageProcessingCommand> context)
    {
        var message = context.Message;
        var handler = message.UserCommand.CommandAction switch
        {
            UserCommandActionEnum.PythonScriptExecuting => new PythonExecuteHandler(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var notificationMessage = await handler.ExecuteAsync(message.SourceMessageInfo);
        var notificationEvent = new DiscordMessageNotificationEvent()
        {
            SourceMessageInfo = message.SourceMessageInfo,
            NotificationMessage = notificationMessage
        };

        await context.Publish(notificationEvent);
    }
}