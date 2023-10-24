using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using HellBotLib.IO;

namespace HellBotLib.Checks;

/// <summary>
/// Custom attribute for checking if the user executing a command is a guild moderator.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class SlashRequireGuildModeratorAttribute : SlashCheckBaseAttribute
{
    public override async Task<bool> ExecuteChecksAsync(InteractionContext ctx)
    {
        // Check if the interaction is in a guild context
        if (ctx.Guild == null)
        {
            return false;
        }

        // Check if the user is the owner of the guild
        if (ctx.Guild.OwnerId == ctx.Member.Id)
        {
            return true;
        }

        // Check if the user has the 'Administrator' permission
        SlashRequirePermissionsAttribute permissionCheck = new(DSharpPlus.Permissions.Administrator);
        if (await permissionCheck.ExecuteChecksAsync(ctx))
        {
            return true;
        }

        SlashRequireGuildAdminAttribute adminCheck = new();
        if (await adminCheck.ExecuteChecksAsync(ctx))
        {
            return true;
        }

        // Check if the user has a specified moderator role defined in the guild's configuration
        GuildConfig? guildConfig = await ConfigManager.GetGuildAsync<GuildConfig>(ctx.Guild.Id);

        if (guildConfig == null || guildConfig.ModeratorRole == 0)
        {
            return false;
        }

        var role = ctx.Guild.GetRole(guildConfig.ModeratorRole);

        return role != null && ctx.Member.Roles.Contains(role);
    }
}

/// <summary>
/// Custom attribute for checking if the user interacting with a context menu command is a guild moderator.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequireGuildModeratorAttribute : ContextMenuCheckBaseAttribute
{
    public override async Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        // Check if the interaction is in a guild context
        if (ctx.Guild == null)
        {
            return false;
        }

        // Check if the user is the owner of the guild
        if (ctx.Guild.OwnerId == ctx.Member.Id)
        {
            return true;
        }

        // Check if the user has the 'Administrator' permission
        ContextMenuRequirePermissionsAttribute permissionCheck = new(DSharpPlus.Permissions.Administrator);
        if (await permissionCheck.ExecuteChecksAsync(ctx))
        {
            return true;
        }

        ContextMenuRequireGuildAdminAttribute adminCheck = new();
        if (await adminCheck.ExecuteChecksAsync(ctx))
        {
            return true;
        }

        // Check if the user has a specified moderator role defined in the guild's configuration
        GuildConfig? guildConfig = await ConfigManager.GetGuildAsync<GuildConfig>(ctx.Guild.Id);

        if (guildConfig == null || guildConfig.ModeratorRole == 0)
        {
            return false;
        }

        var role = ctx.Guild.GetRole(guildConfig.ModeratorRole);

        return role != null && ctx.Member.Roles.Contains(role);
    }
}
