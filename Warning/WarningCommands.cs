using DSharpPlus.SlashCommands;
using HellBotLib.Checks;

namespace Warning;

[SlashCommandGroup("Warning", "Handle all the warnings on the server")]
public class WarningCommands : ApplicationCommandModule
{
    [ContextMenu(DSharpPlus.ApplicationCommandType.UserContextMenu, "Warn User")]
    [ContextMenuRequireGuildModerator]
    public async Task WarnUserMenu(ContextMenuContext ctx)
    {

    }

    [ContextMenu(DSharpPlus.ApplicationCommandType.MessageContextMenu, "Warn Message")]
    [ContextMenuRequireGuildModerator]
    public async Task WarnMessageMenu(ContextMenuContext ctx)
    {

    }
}
