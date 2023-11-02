using DSharpPlus;
using HellBotLib;
using Serilog;
using System.Reflection;

internal static class EventRegister
{
    private static readonly List<IClientEvents> EventTargets = new();

    public static void RegisterEventsForModule(ref DiscordShardedClient client, string moduleName)
    {
        var assembly = Assembly.LoadFrom(moduleName);
        Log.Information("Linking events for {ModuleName}", Path.GetFileName(moduleName));

        IClientEvents[] events = ExtractEventTargets(assembly);

        foreach (var evTarget in events)
        {
            LinkEvents(ref client, evTarget);
            EventTargets.Add(evTarget);
        }
    }

    private static void LinkEvents(ref DiscordShardedClient client, IClientEvents events)
    {
        client.ChannelCreated += events.ChannelCreated;
        client.ChannelDeleted += events.ChannelDeleted;
        client.ChannelPinsUpdated += events.ChannelPinsUpdated;
        client.ChannelUpdated += events.ChannelUpdated;
        client.ClientErrored += events.ClientErrored;
        client.ComponentInteractionCreated += events.ComponentInteractionCreated;
        client.ContextMenuInteractionCreated += events.ContextMenuInteractionCreated;
        client.DmChannelDeleted += events.DmChannelDeleted;
        client.GuildAvailable += events.GuildAvailable;
        client.GuildBanAdded += events.GuildBanAdded;
        client.GuildBanRemoved += events.GuildBanRemoved;
        client.GuildCreated += events.GuildCreated;
        client.GuildDeleted += events.GuildDeleted;
        client.GuildDownloadCompleted += events.GuildDownloadCompleted;
        client.GuildEmojisUpdated += events.GuildEmojisUpdated;
        client.GuildIntegrationsUpdated += events.GuildIntegrationsUpdated;
        client.GuildMemberAdded += events.GuildMemberAdded;
        client.GuildMemberRemoved += events.GuildMemberRemoved;
        client.GuildMembersChunked += events.GuildMembersChunked;
        client.GuildMemberUpdated += events.GuildMemberUpdated;
        client.GuildRoleCreated += events.GuildRoleCreated;
        client.GuildRoleDeleted += events.GuildRoleDeleted;
        client.GuildRoleUpdated += events.GuildRoleUpdated;
        client.GuildStickersUpdated += events.GuildStickersUpdated;
        client.GuildUnavailable += events.GuildUnavailable;
        client.GuildUpdated += events.GuildUpdated;
        client.Heartbeated += events.Heartbeated;
        client.IntegrationCreated += events.IntegrationCreated;
        client.IntegrationDeleted += events.IntegrationDeleted;
        client.IntegrationUpdated += events.IntegrationUpdated;
        client.InteractionCreated += events.InteractionCreated;
        client.InviteCreated += events.InviteCreated;
        client.InviteDeleted += events.InviteDeleted;
        client.MessageCreated += events.MessageCreated;
        client.MessageDeleted += events.MessageDeleted;
        client.MessageReactionAdded += events.MessageReactionAdded;
        client.MessageReactionRemoved += events.MessageReactionRemoved;
        client.MessageReactionRemovedEmoji += events.MessageReactionRemovedEmoji;
        client.MessageReactionsCleared += events.MessageReactionsCleared;
        client.MessagesBulkDeleted += events.MessagesBulkDeleted;
        client.MessageUpdated += events.MessageUpdated;
        client.ModalSubmitted += events.ModalSubmitted;
        client.PresenceUpdated += events.PresenceUpdated;
        client.Ready += events.Ready;
        client.Resumed += events.Resumed;
        client.SocketClosed += events.SocketClosed;
        client.SocketErrored += events.SocketErrored;
        client.SocketOpened += events.SocketOpened;
        client.StageInstanceCreated += events.StageInstanceCreated;
        client.StageInstanceDeleted += events.StageInstanceDeleted;
        client.StageInstanceUpdated += events.StageInstanceUpdated;
        client.ThreadCreated += events.ThreadCreated;
        client.ThreadDeleted += events.ThreadDeleted;
        client.ThreadListSynced += events.ThreadListSynced;
        client.ThreadMembersUpdated += events.ThreadMembersUpdated;
        client.ThreadMemberUpdated += events.ThreadMemberUpdated;
        client.ThreadUpdated += events.ThreadUpdated;
        client.TypingStarted += events.TypingStarted;
        client.UnknownEvent += events.UnknownEvent;
        client.UserSettingsUpdated += events.UserSettingsUpdated;
        client.UserUpdated += events.UserUpdated;
        client.VoiceServerUpdated += events.VoiceServerUpdated;
        client.VoiceStateUpdated += events.VoiceStateUpdated;
        client.WebhooksUpdated += events.WebhooksUpdated;
        client.Zombied += events.Zombied;
    }

    private static IClientEvents[] ExtractEventTargets(Assembly assembly)
    {
        List<IClientEvents> eventTargets = new();

        foreach (Type type in assembly.GetTypes())
        {
            if (typeof(IClientEvents).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                // Create an instance of the type that implements IClientEvents
                if (Activator.CreateInstance(type) is not IClientEvents eventTarget) continue;
                eventTargets.Add(eventTarget);
            }
        }

        return eventTargets.ToArray();
    }
}
