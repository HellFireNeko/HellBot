using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using HellBotLib.Checks;
using HellBotLib.IO;

namespace Warning;

[SlashCommandGroup("Warning", "Handle all the warnings on the server")]
public class WarningCommands : ApplicationCommandModule
{
    [SlashCommand("Warn", "Warns the user")]
    [SlashRequireGuildModerator]
    public async Task WarnUserCommand(InteractionContext ctx, [Option("User", "The user to warn")] DiscordUser user, [Option("Reason", "The reason for the warning!")] string reason)
    {
        await ctx.DeferAsync();

        var config = await ConfigManager.GetGuildAsync<WarningGuildConfig>(ctx.Guild.Id);

        if (config == null) config = new WarningGuildConfig();

        config.GuildUsers.TryGetValue(user.Id, out WarningGuildConfig.UserWarnings? warnings);

        if (warnings == null)
        {
            warnings = new WarningGuildConfig.UserWarnings();
            config.GuildUsers.Add(user.Id, warnings);
        }

        warnings.Warnings.Add(warnings.Warnings.Count, reason);

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Warned the user! They are on {warnings.Warnings.Count}/{config.MaxWarnings} warnings!"));

        if (warnings.Warnings.Count >= config.MaxWarnings && config.BanOnLimit)
        {
            var member = await ctx.Guild.GetMemberAsync(user.Id);
            await ctx.Guild.BanMemberAsync(member, 0, "The user exceeded the warning limit!");

            await ctx.FollowUpAsync(new DiscordFollowupMessageBuilder().WithContent("They exceeded the limit and have now been banned!");
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
