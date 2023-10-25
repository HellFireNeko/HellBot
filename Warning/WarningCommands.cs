using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using HellBotLib.Checks;
using HellBotLib.IO;

namespace Warning;

/// <summary>
/// TODO:
/// <list type="bullet">
///     <item>Setup all the configure commands</item>
///     <item>Setup all the methods of warning people</item>
///     <item>Test all the commands.</item>
/// </list>
/// </summary>
[SlashCommandGroup("Warning", "Handle all the warnings on the server")]
public class WarningCommands : ApplicationCommandModule
{
    [ContextMenu(DSharpPlus.ApplicationCommandType.UserContextMenu, "Warn User")]
    [ContextMenuRequireGuildModerator]
    public async Task WarnUserMenu(ContextMenuContext ctx)
    {
        await ctx.DeferAsync();
    }

    [ContextMenu(DSharpPlus.ApplicationCommandType.MessageContextMenu, "Warn Message")]
    [ContextMenuRequireGuildModerator]
    public async Task WarnMessageMenu(ContextMenuContext ctx)
    {
        await ctx.DeferAsync();
    }

    [SlashCommand("Warn", "Warns the user")]
    [SlashRequireGuildModerator]
    public async Task WarnUserCommand(InteractionContext ctx, [Option("User", "The user to warn")] DiscordMember member, [Option("Reason", "The reason for the warning!")] string reason)
    {
        await ctx.DeferAsync();

        var config = await ConfigManager.GetGuildAsync<WarningGuildConfig>(ctx.Guild.Id);

        if (config == null) config = new WarningGuildConfig();

        config.GuildUsers.TryGetValue(member.Id, out WarningGuildConfig.UserWarnings? warnings);

        if (warnings == null)
        {
            warnings = new WarningGuildConfig.UserWarnings();
            config.GuildUsers.Add(member.Id, warnings);
        }

        warnings.Warnings.Add(warnings.Warnings.Count, reason);

        await ctx.EditResponseAsync(new DiscordWebhookBuilder()
            .WithContent($"Warned the user! They are on {warnings.Warnings.Count}/{config.MaxWarnings} warnings!"));

        await member.SendMessageAsync($"You have been warned by {ctx.Member.Username}, their reason is:\n`{reason}`");

        if (warnings.Warnings.Count >= config.MaxWarnings && config.BanOnLimit)
        {
            await ctx.Guild.BanMemberAsync(member, 0, "The user exceeded the warning limit!");

            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder()
                .WithContent("They exceeded the limit and have now been banned!"));
        }

        await ConfigManager.StoreGuildAsync(ctx.Guild.Id, config);
    }

    [SlashCommandGroup("Configure", "Lets you configure the warning system")]
    public class ConfigureGroup : ApplicationCommandModule
    {
        [SlashCommand("BanOnLimit", "Sets if users should get banned upon reaching the limit")]
        public async Task BanOnLimitCommand(InteractionContext ctx, [Option("Setting", "The setting you desire it to be on")] bool setting)
        {

        }
    }
}
