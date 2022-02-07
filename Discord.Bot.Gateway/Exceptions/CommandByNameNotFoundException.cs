namespace Discord.Bot.Gateway.Exceptions;

public class CommandByNameNotFoundException: Exception
{
    public CommandByNameNotFoundException(string? message)
        :base(message)
    {
        
    }
}