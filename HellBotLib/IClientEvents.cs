using DSharpPlus;
using DSharpPlus.EventArgs;

namespace HellBotLib;

/// <summary>
/// Be careful to not take too long when handeling a event, if you need to, use <see cref="Task.Run(Action)"/> to offload your code.
/// </summary>
public interface IClientEvents
{
    Task ChannelCreated(DiscordClient sender, ChannelCreateEventArgs args) => Task.CompletedTask;
    Task ChannelDeleted(DiscordClient sender, ChannelDeleteEventArgs args) => Task.CompletedTask;
    Task ChannelPinsUpdated(DiscordClient sender, ChannelPinsUpdateEventArgs args) => Task.CompletedTask;
    Task ChannelUpdated(DiscordClient sender, ChannelUpdateEventArgs args) => Task.CompletedTask;
    Task ClientErrored(DiscordClient sender, ClientErrorEventArgs args) => Task.CompletedTask;
    Task ComponentInteractionCreated(DiscordClient sender, ComponentInteractionCreateEventArgs args) => Task.CompletedTask;
    Task ContextMenuInteractionCreated(DiscordClient sender, ContextMenuInteractionCreateEventArgs args) => Task.CompletedTask;
    Task DmChannelDeleted(DiscordClient sender, DmChannelDeleteEventArgs args) => Task.CompletedTask;
    Task GuildAvailable(DiscordClient sender, GuildCreateEventArgs args) => Task.CompletedTask;
    Task GuildBanAdded(DiscordClient sender, GuildBanAddEventArgs args) => Task.CompletedTask;
    Task GuildBanRemoved(DiscordClient sender, GuildBanRemoveEventArgs args) => Task.CompletedTask;
    Task GuildCreated(DiscordClient sender, GuildCreateEventArgs args) => Task.CompletedTask;
    Task GuildDeleted(DiscordClient sender, GuildDeleteEventArgs args) => Task.CompletedTask;
    Task GuildDownloadCompleted(DiscordClient sender, GuildDownloadCompletedEventArgs args) => Task.CompletedTask;
    Task GuildEmojisUpdated(DiscordClient sender, GuildEmojisUpdateEventArgs args) => Task.CompletedTask;
    Task GuildIntegrationsUpdated(DiscordClient sender, GuildIntegrationsUpdateEventArgs args) => Task.CompletedTask;
    Task GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs args) => Task.CompletedTask;
    Task GuildMemberRemoved(DiscordClient sender, GuildMemberRemoveEventArgs args) => Task.CompletedTask;
    Task GuildMembersChunked(DiscordClient sender, GuildMembersChunkEventArgs args) => Task.CompletedTask;
    Task GuildMemberUpdated(DiscordClient sender, GuildMemberUpdateEventArgs args) => Task.CompletedTask;
    Task GuildRoleCreated(DiscordClient sender, GuildRoleCreateEventArgs args) => Task.CompletedTask;
    Task GuildRoleDeleted(DiscordClient sender, GuildRoleDeleteEventArgs args) => Task.CompletedTask;
    Task GuildRoleUpdated(DiscordClient sender, GuildRoleUpdateEventArgs args) => Task.CompletedTask;
    Task GuildStickersUpdated(DiscordClient sender, GuildStickersUpdateEventArgs args) => Task.CompletedTask;
    Task GuildUnavailable(DiscordClient sender, GuildDeleteEventArgs args) => Task.CompletedTask;
    Task GuildUpdated(DiscordClient sender, GuildUpdateEventArgs args) => Task.CompletedTask;
    Task Heartbeated(DiscordClient sender, HeartbeatEventArgs args) => Task.CompletedTask;
    Task IntegrationCreated(DiscordClient sender, IntegrationCreateEventArgs args) => Task.CompletedTask;
    Task IntegrationDeleted(DiscordClient sender, IntegrationDeleteEventArgs args) => Task.CompletedTask;
    Task IntegrationUpdated(DiscordClient sender, IntegrationUpdateEventArgs args) => Task.CompletedTask;
    Task InteractionCreated(DiscordClient sender, InteractionCreateEventArgs args) => Task.CompletedTask;
    Task InviteCreated(DiscordClient sender, InviteCreateEventArgs args) => Task.CompletedTask;
    Task InviteDeleted(DiscordClient sender, InviteDeleteEventArgs args) => Task.CompletedTask;
    Task MessageCreated(DiscordClient sender, MessageCreateEventArgs args) => Task.CompletedTask;
    Task MessageDeleted(DiscordClient sender, MessageDeleteEventArgs args) => Task.CompletedTask;
    Task MessageReactionAdded(DiscordClient sender, MessageReactionAddEventArgs args) => Task.CompletedTask;
    Task MessageReactionRemoved(DiscordClient sender, MessageReactionRemoveEventArgs args) => Task.CompletedTask;
    Task MessageReactionRemovedEmoji(DiscordClient sender, MessageReactionRemoveEmojiEventArgs args) => Task.CompletedTask;
    Task MessageReactionsCleared(DiscordClient sender, MessageReactionsClearEventArgs args) => Task.CompletedTask;
    Task MessagesBulkDeleted(DiscordClient sender, MessageBulkDeleteEventArgs args) => Task.CompletedTask;
    Task MessageUpdated(DiscordClient sender, MessageUpdateEventArgs args) => Task.CompletedTask;
    Task ModalSubmitted(DiscordClient sender, ModalSubmitEventArgs args) => Task.CompletedTask;
    Task PresenceUpdated(DiscordClient sender, PresenceUpdateEventArgs args) => Task.CompletedTask;
    Task Ready(DiscordClient sender, ReadyEventArgs args) => Task.CompletedTask;
    Task Resumed(DiscordClient sender, ReadyEventArgs args) => Task.CompletedTask;
    Task SocketClosed(DiscordClient sender, SocketCloseEventArgs args) => Task.CompletedTask;
    Task SocketErrored(DiscordClient sender, SocketErrorEventArgs args) => Task.CompletedTask;
    Task SocketOpened(DiscordClient sender, SocketEventArgs args) => Task.CompletedTask;
    Task StageInstanceCreated(DiscordClient sender, StageInstanceCreateEventArgs args) => Task.CompletedTask;
    Task StageInstanceDeleted(DiscordClient sender, StageInstanceDeleteEventArgs args) => Task.CompletedTask;
    Task StageInstanceUpdated(DiscordClient sender, StageInstanceUpdateEventArgs args) => Task.CompletedTask;
    Task ThreadCreated(DiscordClient sender, ThreadCreateEventArgs args) => Task.CompletedTask;
    Task ThreadDeleted(DiscordClient sender, ThreadDeleteEventArgs args) => Task.CompletedTask;
    Task ThreadListSynced(DiscordClient sender, ThreadListSyncEventArgs args) => Task.CompletedTask;
    Task ThreadMembersUpdated(DiscordClient sender, ThreadMembersUpdateEventArgs args) => Task.CompletedTask;
    Task ThreadMemberUpdated(DiscordClient sender, ThreadMemberUpdateEventArgs args) => Task.CompletedTask;
    Task ThreadUpdated(DiscordClient sender, ThreadUpdateEventArgs args) => Task.CompletedTask;
    Task TypingStarted(DiscordClient sender, TypingStartEventArgs args) => Task.CompletedTask;
    Task UnknownEvent(DiscordClient sender, UnknownEventArgs args) => Task.CompletedTask;
    Task UserSettingsUpdated(DiscordClient sender, UserSettingsUpdateEventArgs args) => Task.CompletedTask;
    Task UserUpdated(DiscordClient sender, UserUpdateEventArgs args) => Task.CompletedTask;
    Task VoiceServerUpdated(DiscordClient sender, VoiceServerUpdateEventArgs args) => Task.CompletedTask;
    Task VoiceStateUpdated(DiscordClient sender, VoiceStateUpdateEventArgs args) => Task.CompletedTask;
    Task WebhooksUpdated(DiscordClient sender, WebhooksUpdateEventArgs args) => Task.CompletedTask;
    Task Zombied(DiscordClient sender, ZombiedEventArgs args) => Task.CompletedTask;
}
