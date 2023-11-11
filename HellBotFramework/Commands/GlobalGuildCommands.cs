using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using HellBotLib;
using HellBotLib.Checks;
using HellBotLib.IO;

namespace HellBotFramework.Commands;

internal class GlobalGuildCommands : ApplicationCommandModule
{
    [SlashCommand("RoleSetup", "Sets the admin role")]
    [SlashRequireGuild]
    [SlashRequireGuildOwner]
    public async Task SetAdminRole(InteractionContext ctx, [Option("admin", "The role for admins")] DiscordRole role, [Option("moderator", "The role for admins")] DiscordRole modrole)
    {
        await ctx.DeferAsync(true);

        var config = await ConfigManager.GetGuildAsync<GuildConfig>(ctx.Guild.Id);
        config ??= new GuildConfig();

        config.AdminRole = role.Id;
        config.ModeratorRole = modrole.Id;

        await ConfigManager.StoreGuildAsync(ctx.Guild.Id, config);

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Done"));
    }
}
