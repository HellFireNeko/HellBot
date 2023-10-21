using HellBotLib;

internal class BotConfig : IBotConfig
{
    public string Token;
    public string DefaultPresence;

    public BotConfig()
    {
        Token = string.Empty;
        DefaultPresence = string.Empty;
    }
}
