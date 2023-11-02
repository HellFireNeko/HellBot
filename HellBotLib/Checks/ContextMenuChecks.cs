using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace HellBotLib.Checks;

/// <summary>
/// Custom attribute for checking if the user interacting with a context menu command is the owner of the guild.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequireGuildOwnerAttribute : ContextMenuCheckBaseAttribute
{
    public override Task<bool> ExecuteChecksAsync(ContextMenuContext ctx) => Task.FromResult(ctx.Guild != null && (ctx.Guild.OwnerId == ctx.Member.Id));
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequireBotPermissionsAttribute : ContextMenuCheckBaseAttribute
{
    public Permissions Permissions { get; }

    public bool IgnoreDms { get; } = true;

    public ContextMenuRequireBotPermissionsAttribute(Permissions permissions, bool ignoreDms = true)
    {
        Permissions = permissions;
        IgnoreDms = ignoreDms;
    }


    public override async Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        if (ctx.Guild == null) return IgnoreDms;

        DiscordMember bot = await ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id);

        if (bot == null) return false;

        if (bot.Id == ctx.Guild.OwnerId) return true;

        Permissions pbot = ctx.Channel.PermissionsFor(bot);

        return (pbot & Permissions.Administrator) != 0 || (pbot & Permissions) == Permissions;
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequireGuildAttribute : ContextMenuCheckBaseAttribute
{
    public override Task<bool> ExecuteChecksAsync(ContextMenuContext ctx) => Task.FromResult(ctx.Guild != null);
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequireOwnerAttribute : ContextMenuCheckBaseAttribute
{
    public override Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        DiscordApplication app = ctx.Client.CurrentApplication;
        DiscordUser me = ctx.Client.CurrentUser;

        return app != null ? Task.FromResult(app.Owners.Any(x => x.Id == ctx.User.Id)) : Task.FromResult(ctx.User.Id == me.Id);
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequirePermissionsAttribute : ContextMenuCheckBaseAttribute
{
    public Permissions Permissions { get; }

    public bool IgnoreDms { get; } = true;

    public ContextMenuRequirePermissionsAttribute(Permissions permissions, bool ignoreDms = true)
    {
        this.Permissions = permissions;
        this.IgnoreDms = ignoreDms;
    }

    public override async Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        if (ctx.Guild == null)
        {
            return this.IgnoreDms;
        }

        DiscordMember usr = ctx.Member;
        if (usr == null)
        {
            return false;
        }

        Permissions pusr = ctx.Channel.PermissionsFor(usr);

        DiscordMember bot = await ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id);
        if (bot == null)
        {
            return false;
        }

        Permissions pbot = ctx.Channel.PermissionsFor(bot);

        bool usrok = ctx.Guild.OwnerId == usr.Id;
        bool botok = ctx.Guild.OwnerId == bot.Id;

        if (!usrok)
        {
            usrok = (pusr & Permissions.Administrator) != 0 || (pusr & this.Permissions) == this.Permissions;
        }

        if (!botok)
        {
            botok = (pbot & Permissions.Administrator) != 0 || (pbot & this.Permissions) == this.Permissions;
        }

        return usrok && botok;
    }
}

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class ContextMenuRequireUserPermissionsAttribute : ContextMenuCheckBaseAttribute
{
    public Permissions Permissions { get; }

    public bool IgnoreDms { get; } = true;

    public ContextMenuRequireUserPermissionsAttribute(Permissions permissions, bool ignoreDms = true)
    {
        this.Permissions = permissions;
        this.IgnoreDms = ignoreDms;
    }

    public override Task<bool> ExecuteChecksAsync(ContextMenuContext ctx)
    {
        if (ctx.Guild == null)
        {
            return Task.FromResult(this.IgnoreDms);
        }

        DiscordMember usr = ctx.Member;
        if (usr == null)
        {
            return Task.FromResult(false);
        }

        if (usr.Id == ctx.Guild.OwnerId)
        {
            return Task.FromResult(true);
        }

        Permissions pusr = ctx.Channel.PermissionsFor(usr);

        return (pusr & Permissions.Administrator) != 0
            ? Task.FromResult(true)
            : (pusr & this.Permissions) == this.Permissions ? Task.FromResult(true) : Task.FromResult(false);
    }
}