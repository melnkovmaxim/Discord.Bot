using Discord.Bot.Shared.Commands;
using Discord.Bot.Shared.Models;

namespace Discord.Bot.TextCommandHandling;

public interface IHandler
{
    Task<string> ExecuteAsync(SourceMessageInfo sourceMessageInfo);
}