using DSharpPlus;
using DSharpPlus.EventArgs;
using HellBotLib;
using HellBotLib.IO;

namespace Warning;

public class WarningEvents : IClientEvents
{
    public Task GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs args) 
    {
        Task.Run(async () =>
        {
            var config = await ConfigManager.GetGuildAsync<WarningGuildConfig>(args.Guild.Id);

            if (config == null)
            {
                return;
            }
        });

        return Task.CompletedTask;
    }
}
