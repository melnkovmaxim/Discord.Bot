namespace Discord.Bot.Gateway.Exceptions;

public class NotFoundEndpointException: Exception
{
    public NotFoundEndpointException(string? message)
        :base(message)
    {
        
    }
}