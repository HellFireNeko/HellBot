using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using HellBotLib.IO;

namespace HellBotLib.Checks;

/// <summary>
/// Custom attribute for checking if the user executing a command is a guild admin.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class SlashRequireGuildAdminAttribute : SlashCheckBaseAttribute
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

        // Check if the user has a specified admin role defined in the guild's configuration
        GuildConfig guildConfig = await ConfigManager.GetGuildAsync<GuildConfig>(ctx.Guild.Id);

        if (guildConfig.AdminRole == 0)
        {
            return false;
        }

        var role = ctx.Guild.GetRole(guildConfig.AdminRole);

        if (role == null || !ctx.Member.Roles.Contains(role))
        {
            return false;
        }

        return true;
    }
}

/// <summary>
/// Custom attribute for checking if the user interacting with a context menu command is a guild admin.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequireGuildAdminAttribute : ContextMenuCheckBaseAttribute
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

        // Check if the user has a specified admin role defined in the guild's configuration
        GuildConfig guildConfig = await ConfigManager.GetGuildAsync<GuildConfig>(ctx.Guild.Id);

        if (guildConfig.AdminRole == 0)
        {
            return false;
        }

        var role = ctx.Guild.GetRole(guildConfig.AdminRole);

        if (role == null || !ctx.Member.Roles.Contains(role))
        {
            return false;
        }

        return true;
    }
}
