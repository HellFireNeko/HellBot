using HellBotLib;

namespace Warnings;

public class WarningsGuildConfig : IGuildConfig
{
    public Dictionary<ulong, MemberWarnings> Members = new();
    public bool BanOnLimit = true;
    public uint MaxWarningsAllowed = 3;
    public bool DmOnWarning = true;
    public bool DmOnBan = true;
    
}

public class MemberWarnings
{
    public List<Warning> Warnings = new();
}

public class Warning
{
    /// <summary>
    /// Why was the warning created?
    /// </summary>
    public string Reason = string.Empty;
    /// <summary>
    /// Who created the warning?
    /// </summary>
    public ulong WarningAdministrator;

    public Warning(string reason, ulong warningAdministrator)
    {
        Reason = reason;
        WarningAdministrator = warningAdministrator;
    }
}