using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using HellBotLib.IO;

internal class Core : ApplicationCommandModule
{
    public static bool KeepRunning = true;
    public static bool FixAll = false;

    [SlashCommand("shutdown", "Stops the bot from taking over the world")]
    [SlashRequireOwner]
    public async Task Shutdown(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync("Goodbye cruel world", true);

        KeepRunning = false;

        await ctx.Client.DisconnectAsync();
    }

    [SlashCommand("fix", "Refreshes everything")]
    [SlashRequireOwner]
    public async Task Fix(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync("Fixing, and shutting down", true);

        FixAll = true;
        KeepRunning = false;
    }

    [SlashCommand("Ping", "Gets the delay of the bot!")]
    public async Task Ping(InteractionContext ctx)
    {
        await ctx.DeferAsync();

        var ping = ctx.Client.Ping;

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Pong! {ping}ms"));
    }
}
