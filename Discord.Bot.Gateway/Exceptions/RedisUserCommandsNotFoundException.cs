namespace Discord.Bot.Gateway.Exceptions;

public class RedisUserCommandsNotFoundException: Exception
{
    public RedisUserCommandsNotFoundException(string? message)
        :base(message)
    {
        
    }
}