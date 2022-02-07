using MassTransit;
using Microsoft.Extensions.Hosting;

namespace Discord.Bot.TextCommandHandling;

public class BusService: IHostedService
{
    private readonly IBusControl _bus;

    public BusService(IBusControl bus)
    {
        _bus = bus;
    }
    
    public Task StartAsync(CancellationToken cancellationToken) => _bus.StartAsync(cancellationToken);
    public Task StopAsync(CancellationToken cancellationToken) => _bus.StopAsync(cancellationToken);
}