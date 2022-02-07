using System.Diagnostics;
using System.Text.Json;
using Discord.Bot.Gateway.Exceptions;
using Discord.Bot.Shared.Commands;
using Discord.Bot.Shared.Constants;
using Discord.Bot.Shared.Enums;
using Discord.Bot.Shared.Extensions;
using Discord.Bot.Shared.Models;
using Discord.WebSocket;
using IronPython.Hosting;
using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;

namespace Discord.Bot.Gateway;

public class Worker : IHostedService, IAsyncDisposable
{
    private readonly IBusControl _bus;
    private readonly DiscordSocketClient _discordClient;

    public Worker(IBusControl bus)
    {
        _bus = bus;
        _discordClient = new DiscordSocketClient();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _discordClient.MessageReceived += async (message) =>
        {
            try
            {
                //var availableSerializedCommands = await _distributedCache.GetStringAsync(RedisConstants.UserCommandsEntryName, cancellationToken);
                var commands = new List<UserCommand>()
                {
                    new UserCommand()
                    {
                        Name = "!python",
                        CommandAction = UserCommandActionEnum.PythonScriptExecuting,
                        CommandType = UserCommandTypeEnum.Text
                    }
                };
                var availableSerializedCommands = JsonSerializer.Serialize(commands);

                if (string.IsNullOrEmpty(availableSerializedCommands))
                {
                    throw new RedisUserCommandsNotFoundException(
                        $"User commands missing in redis by entry key {RedisConstants.UserCommandsEntryName}");
                }

                var availableCommands = JsonSerializer.Deserialize<List<UserCommand>>(availableSerializedCommands);
                var desiredCommand = availableCommands?.SingleOrDefault(x => x.Name == message.Content);

                if (desiredCommand is null)
                {
                    throw new CommandByNameNotFoundException(
                        $"Command by name {message.Content} not found in commands list or found greater then single");
                }

                var sourceMessageInfo = new SourceMessageInfo()
                    { ChannelId = message.Channel.Id, SenderUsername = message.Author.Username, Message = message.Content };
                var publishingMessage = new DiscordMessageProcessingCommand()
                {
                    SourceMessageInfo = sourceMessageInfo,
                    UserCommand = desiredCommand
                };

                var endpoint = publishingMessage.UserCommand.CommandType switch
                {
                    UserCommandTypeEnum.Text => await _bus.GetDiscordMessageCommandTextEndpointAsync(),
                    UserCommandTypeEnum.Media => throw new NotImplementedException(),
                    _ => throw new NotFoundEndpointException(
                        $"Not found endpoint for specific command type {publishingMessage.UserCommand.CommandType.ToString()}")
                };

                await endpoint.Send(publishingMessage, cancellationToken);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        };

        await _discordClient.LoginAsync(TokenType.Bot, Constants.DiscordToken);
        await _discordClient.StartAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken) => _discordClient.StopAsync();

    public ValueTask DisposeAsync() => _discordClient.DisposeAsync();
}