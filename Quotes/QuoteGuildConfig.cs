using HellBotLib;

namespace Quotes;

public class QuoteGuildConfig : IGuildConfig
{
    public ulong ChannelID;
    public bool AllowNSFW;

    public static QuoteGuildConfig Default => new(0, false);

    public QuoteGuildConfig(ulong channelID, bool allowNSFW)
    {
        ChannelID = channelID;
        AllowNSFW = allowNSFW;
    }
}
