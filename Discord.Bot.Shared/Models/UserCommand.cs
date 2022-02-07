using Discord.Bot.Shared.Enums;

namespace Discord.Bot.Shared.Models;

public record UserCommand
{
    public string Name { get; set; } = null!;
    public UserCommandTypeEnum CommandType { get; set; }
    public UserCommandActionEnum CommandAction { get; set; }
    public string? ChannelName { get; set; }
    public string? Description { get; set; }
}