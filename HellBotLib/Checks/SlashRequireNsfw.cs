using DSharpPlus.SlashCommands;

namespace HellBotLib.Checks;

/// <summary>
/// Defines that usage of this command is restricted to NSFW channels.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class SlashRequireNsfwAttribute : SlashCheckBaseAttribute
{
    public override Task<bool> ExecuteChecksAsync(InteractionContext ctx)
        => Task.FromResult(ctx.Channel.Guild == null || ctx.Channel.IsNSFW);
}
