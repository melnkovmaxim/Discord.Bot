using Discord.Bot.Shared.Models;

namespace Discord.Bot.Shared.Commands;

public record DiscordMessageProcessingCommand
{
    public SourceMessageInfo SourceMessageInfo { get; set; } = null!;
    public UserCommand UserCommand { get; set; } = null!;
}