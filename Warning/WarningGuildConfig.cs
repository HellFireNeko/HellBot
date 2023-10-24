using HellBotLib;

namespace Warning;

public class WarningGuildConfig : IGuildConfig
{
    public Dictionary<ulong, UserWarnings> GuildUsers = new();
    public int MaxWarnings = 3;
    public bool BanOnLimit = false;
    public bool BanOnReturn = false;
    public bool TimeoutOnWarn = true;
    public ulong TimeoutBase = 300;
    public ulong TimeoutMultiplier = 2;

    public class UserWarnings
    {
        public Dictionary<int, string> Warnings = new();
    }
}