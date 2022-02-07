using Discord.Bot.Gateway;
using Discord.Bot.Shared.Constants;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit();
        services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
        {
            configure.Host(RabbitMqConstants.RabbitMqUri, "/", c => { });
        }));
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();