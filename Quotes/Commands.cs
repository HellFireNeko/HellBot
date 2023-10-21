using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using HellBotLib.Checks;
using HellBotLib.IO;

namespace Quotes;

[SlashCommandGroup("Quotes", "A group of commands for the Quotes module")]
public class Commands : ApplicationCommandModule
{
    static DiscordColor GetRandomColor()
    {
        Random random = new();

        byte red = (byte)random.Next(256);
        byte green = (byte)random.Next(256);
        byte blue = (byte)random.Next(256);

        return new DiscordColor(red, green, blue);
    }

    [SlashCommand("Set", "Sets the channel to be the quote channel")]
    [SlashRequireGuildModerator]
    public async Task Set(InteractionContext ctx, [Option("Nsfw", "Does it allow nsfw?", true)] bool allowNsfw)
    {
        await ctx.DeferAsync(true);

        var config = QuoteGuildConfig.Default;

        config.ChannelID = ctx.Channel.Id;
        config.AllowNSFW = allowNsfw;

        await ConfigManager.StoreGuildAsync(ctx.Guild.Id, config);

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("This is now the quote channel!"));
    }

    [ContextMenu(ApplicationCommandType.MessageContextMenu, "Quote")]
    [ContextMenuRequireGuild]
    public async Task QuoteMenu(ContextMenuContext ctx)
    {
        await ctx.DeferAsync(true);
        QuoteGuildConfig? config = await ConfigManager.GetGuildAsync<QuoteGuildConfig>(ctx.Guild.Id);

        if (config == null)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("This guild has not set a quote channel!"));
            return;
        }

        if (ctx.Guild.Channels.ContainsKey(config.ChannelID) is false)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The Quote channel no longer exists"));
            return;
        }

        if (config.AllowNSFW is false && ctx.Channel.IsNSFW)
        {
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The server does not allow quoting nsfw messages!"));
            return;
        }

        var channel = ctx.Guild.GetChannel(config.ChannelID);

        var button = new DiscordLinkButtonComponent(ctx.TargetMessage.JumpLink.ToString(), "Jump to message");

        var embed = new DiscordEmbedBuilder()
            .WithTitle("Quoted!")
            .WithDescription($"**{ctx.TargetMessage.Author.Mention} said:**\n{ctx.TargetMessage.Content}")
            .WithColor(GetRandomColor())
            .WithTimestamp(ctx.TargetMessage.Timestamp)
            .WithAuthor(ctx.TargetMessage.Author.Username, null, ctx.TargetMessage.Author.AvatarUrl)
            .Build();

        var msgBuilder = new DiscordMessageBuilder()
            .WithEmbed(embed)
            .AddComponents(new[]
            {
                button
            }
        );

        await channel.SendMessageAsync(msgBuilder);
    }
}
