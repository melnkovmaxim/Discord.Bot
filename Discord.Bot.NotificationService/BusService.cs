using Discord.Bot.Shared.Constants;
using Discord.WebSocket;
using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Discord.Bot.Notification;

public class BusService: IHostedService, IAsyncDisposable
{
    private readonly DiscordSocketClient _discordClient;
    private readonly IBusControl _bus;

    public BusService(DiscordSocketClient discordClient, IBusControl bus)
    {
        _discordClient = discordClient;
        _bus = bus;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _discordClient.LoginAsync(TokenType.Bot, DiscordConstants.DiscordToken);
        await _discordClient.StartAsync();
        await _bus.StartAsync(cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _discordClient.StopAsync();
        await _bus.StopAsync(cancellationToken);
    }

    public ValueTask DisposeAsync() => _discordClient.DisposeAsync();
}