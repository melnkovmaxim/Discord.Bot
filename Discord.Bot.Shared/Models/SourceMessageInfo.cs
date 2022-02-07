namespace Discord.Bot.Shared.Models;

public record SourceMessageInfo
{
    public string SenderUsername { get; set; } = null!;
    public ulong ChannelId { get; set; }
    public string Message { get; set; } = null!;
}