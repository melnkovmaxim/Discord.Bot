using Discord.Bot.Shared.Commands;
using Discord.Bot.Shared.Constants;
using Discord.Bot.TextCommandHandling;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(configurator =>
        {
            configurator.AddConsumer<DiscordMessageProcessingCommandConsumer>();
        });
        services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
        {
            configure.Host(RabbitMqConstants.RabbitMqUri, "/");
            configure.ReceiveEndpoint(RabbitMqConstants.DiscordMessageCommandTextQueue, e =>
            {
                e.PrefetchCount = 16;
                e.UseMessageRetry(x => x.Interval(2, TimeSpan.FromSeconds(10)));
                e.Consumer<DiscordMessageProcessingCommandConsumer>(provider);
            });
        }));
        services.AddSingleton<IHostedService, BusService>();
    })
    .Build();

await host.RunAsync();