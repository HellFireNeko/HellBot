using HellBotLib;

namespace Warning;

public class WarningGuildConfig : IGuildConfig
{
    public Dictionary<ulong, uint> Warnings = new();
}
