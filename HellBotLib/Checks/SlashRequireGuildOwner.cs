using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HellBotLib.Checks;

/// <summary>
/// Custom attribute for checking if the user executing a command is the owner of the guild.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class SlashRequireGuildOwnerAttribute : SlashCheckBaseAttribute
{
    public override Task<bool> ExecuteChecksAsync(InteractionContext ctx) => Task.FromResult(ctx.Guild != null && (ctx.Guild.OwnerId == ctx.Member.Id));
}
