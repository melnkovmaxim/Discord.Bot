using Discord.Bot.Notification;
using Discord.Bot.Shared.Constants;
using Discord.WebSocket;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<DiscordSocketClient, DiscordSocketClient>();
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<DiscordMessageNotificationEventConsumer>();
        });
        services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
        {
            configure.Host(RabbitMqConstants.RabbitMqUri, "/");
            configure.ReceiveEndpoint(RabbitMqConstants.DiscordMessageCommandNotificationQueue, e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, TimeSpan.FromSeconds(10)));
                e.Consumer<DiscordMessageNotificationEventConsumer>(provider);
            });
        }));
        services.AddSingleton<IHostedService, BusService>();
    })
    .Build();

await host.RunAsync();