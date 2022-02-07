using Discord.Bot.Shared.Constants;
using MassTransit;

namespace Discord.Bot.Shared.Extensions;

public static class BusControlExtensions
{
    public static async Task<ISendEndpoint> GetDiscordMessageCommandTextEndpointAsync(this IBusControl bus)
    {
        var path = Path.Combine("rabbitmq://other.tabbitmq:5672/", RabbitMqConstants.DiscordMessageCommandTextQueue);
        var rabbitUri = new Uri(path);
        
        return await bus.GetSendEndpoint(rabbitUri);
    }
}