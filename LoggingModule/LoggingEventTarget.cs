using DSharpPlus.EventArgs;
using DSharpPlus;
using HellBotLib;

namespace LoggingModule;

public class LoggingEventTarget : IClientEvents
{
    public Task MessageCreated(DiscordClient sender, MessageCreateEventArgs args)
    {
        Console.WriteLine($"Yall this person {args.Author.Username} said {args.Message.Content}");

        return Task.CompletedTask;
    }
}
