using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using HellBotLib.Checks;
using HellBotLib.IO;

namespace Warnings;

public class Commands : ApplicationCommandModule
{
    //public class WarningUserAutocomplete : IAutocompleteProvider
    //{
    //    public async Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
    //    {
    //        var config = await ConfigManager.GetGuildAsync<WarningsGuildConfig>(ctx.Guild.Id);

    //        if (config == null)
    //        {
    //            return new List<DiscordAutoCompleteChoice>();
    //        }

    //        List<DiscordAutoCompleteChoice> choices = new();

    //        foreach (var user in config.Members)
    //        {
    //            //var member = await ctx.Guild.GetMemberAsync(user.Key);

    //            //if (member == null) continue;

    //            choices.Add(new DiscordAutoCompleteChoice(user.Key.ToString(), (long)user.Key));
    //        }

    //        return choices;
    //    }
    //}

    [SlashCommandGroup("Warning", "Handles all the warning related commands")]
    public class WarningsCommands : ApplicationCommandModule
    {
        [SlashCommand("Warn", "Warns the target user with a reason")]
        [SlashRequireGuildModerator]
        public async Task WarnCommand(InteractionContext ctx, [Option("member", "The member to warn")] DiscordUser user, [Option("reason", "The reason for the warning")] string reason)
        {
            await ctx.DeferAsync(true);

            var config = await ConfigManager.GetGuildAsync<WarningsGuildConfig>(ctx.Guild.Id);

            config ??= new WarningsGuildConfig();

            var member = await ctx.Guild.GetMemberAsync(user.Id);

            if (config.Members.ContainsKey(member.Id))
            {
                config.Members[member.Id].Warnings.Add(new Warning(reason, ctx.Member.Id));
            }
            else
            {
                var memberWarning = new MemberWarnings();
                memberWarning.Warnings.Add(new Warning(reason, ctx.Member.Id));

                config.Members.Add(member.Id, memberWarning);
            }

            if (config.Members[member.Id].Warnings.Count > config.MaxWarningsAllowed)
            {
                if (config.BanOnLimit)
                {
                    if (config.DmOnBan)
                    {
                        var dm = await member.CreateDmChannelAsync();
                        await dm.SendMessageAsync($"Due to your conduct, you have been removed from {ctx.Guild.Name} for violating its rules repeatedly.");
                    }
                    await member.BanAsync(0, "The user exceeded their warn limit");
                }
            }
            else
            {
                if (config.DmOnWarning)
                {
                    var dm = await member.CreateDmChannelAsync();
                    await dm.SendMessageAsync($"You have been issued a warning from {ctx.Member.DisplayName}. Their reason for doing so is \"{reason}\".");
                }
            }

            await ConfigManager.StoreGuildAsync(ctx.Guild.Id, config);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The member has been warned!"));
        }

        [SlashCommand("Pardon", "Pardons a user that hasnt gone too far")]
        [SlashRequireGuildModerator]
        public async Task PardonCommand(InteractionContext ctx, [Option("member", "The member to pardon")] DiscordUser user, [Option("warning", "The warning to pardon")] long warningNumber)
        {
            await ctx.DeferAsync(true);

            var config = await ConfigManager.GetGuildAsync<WarningsGuildConfig>(ctx.Guild.Id);

            if (config == null)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The warnings config is not set up"));
                return;
            }

            if (config.Members.ContainsKey(user.Id) is false)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The user you wanted to pardon does not have a record"));
                return;
            }

            if (warningNumber < 0 || (int)warningNumber >= config.Members[user.Id].Warnings.Count || warningNumber >= config.MaxWarningsAllowed)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("You cant pardon a warning that is out of range"));
                return;
            }

            config.Members[user.Id].Warnings.RemoveAt((int)warningNumber);

            await ConfigManager.StoreGuildAsync(ctx.Guild.Id, config);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Pardoned the warning!"));
        }

        [SlashCommand("View", "Views a users warnings")]
        [SlashRequireGuildModerator]
        public async Task ViewCommand(InteractionContext ctx, [Option("member", "The member to view")] DiscordUser member)
        {
            await ctx.DeferAsync(true);

            var config = await ConfigManager.GetGuildAsync<WarningsGuildConfig>(ctx.Guild.Id);

            if (config == null)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The warnings config is not set up"));
                return;
            }

            if (config.Members.ContainsKey(member.Id) is false)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The user you wanted to view does not have a record"));
                return;
            }

            if (config.Members[member.Id].Warnings.Count == 0)
            {
                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The user has no warnings, but has had a record before"));
                return;
            }

            List<DiscordEmbed> embeds = new List<DiscordEmbed>();

            foreach (var warning in config.Members[member.Id].Warnings)
            {
                var warningAuthor = await ctx.Guild.GetMemberAsync(warning.WarningAdministrator);

                if (warningAuthor != null)
                {
                    embeds.Add(new DiscordEmbedBuilder()
                        .WithAuthor(warningAuthor.DisplayName, null, warningAuthor.AvatarUrl)
                        .WithDescription(warning.Reason)
                        .Build());
                }
                else
                {
                    embeds.Add(new DiscordEmbedBuilder()
                        .WithAuthor("Unable to get the author of the warning")
                        .WithDescription(warning.Reason)
                        .Build());
                }
            }

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbeds(embeds));
        }

        [SlashCommand("Configure", "Configures the warnings module")]
        [SlashRequireGuildAdmin]
        public async Task ConfigureCommand(InteractionContext ctx, [Option("Ban", "Determines if the bot should ban upon a user reaching the limit")] bool ban, [Option("Max", "Sets the max number of warnings a user may get")] long max, [Option("onwarn", "Sets if the bot should attempt to dm on warn")] bool onwarn, [Option("onban", "Sets if the bot should attempt to dm on ban")] bool onban)
        {
            await ctx.DeferAsync(true);

            var config = await ConfigManager.GetGuildAsync<WarningsGuildConfig>(ctx.Guild.Id);

            config ??= new WarningsGuildConfig();

            config.BanOnLimit = ban;
            config.MaxWarningsAllowed = (uint)max;
            config.DmOnWarning = onwarn;
            config.DmOnBan = onban;

            await ConfigManager.StoreGuildAsync(ctx.Guild.Id, config);

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("The config has been updated"));
        }
    }
}
