namespace HellBotLib;

/// <summary>
/// The main guild config, just some basic stuff to get the special checks working!
/// </summary>
public struct GuildConfig : IGuildConfig
{
    public ulong AdminRole = 0;
    public ulong ModeratorRole = 0;

    public GuildConfig()
    {
    }
}
