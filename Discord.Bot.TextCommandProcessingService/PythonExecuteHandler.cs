using Discord.Bot.Shared.Models;

namespace Discord.Bot.TextCommandHandling;

public class PythonExecuteHandler: IHandler
{
    public PythonExecuteHandler()
    {
        
    }
    
    public Task<string> ExecuteAsync(SourceMessageInfo sourceMessageInfo)
    {
        return Task.FromResult("haha im lazy");
    }
}