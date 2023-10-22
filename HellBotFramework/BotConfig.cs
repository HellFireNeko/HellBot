using HellBotLib;

internal class BotConfig : IBotConfig
{
    public string Token = string.Empty;
    public string DefaultPresence = string.Empty;
    public bool PresenceIntent = false;
    public bool ServerMembersIntent = false;
    public bool MessageContentIntent = false;
}
