using DSharpPlus;
using DSharpPlus.EventArgs;

namespace HellBotLib;

/// <summary>
/// Be careful to not take too long when handeling a event, if you need to, use <see cref="Task.Run(Action)"/> to offload your code.
/// </summary>
public interface IClientEvents
{
    Task ChannelCreated(DiscordClient sender, ChannelCreateEventArgs args) { return Task.CompletedTask; }
    Task ChannelDeleted(DiscordClient sender, ChannelDeleteEventArgs args) { return Task.CompletedTask; }
    Task ChannelPinsUpdated(DiscordClient sender, ChannelPinsUpdateEventArgs args) { return Task.CompletedTask; }
    Task ChannelUpdated(DiscordClient sender, ChannelUpdateEventArgs args) { return Task.CompletedTask; }
    Task ClientErrored(DiscordClient sender, ClientErrorEventArgs args) { return Task.CompletedTask; }
    Task ComponentInteractionCreated(DiscordClient sender, ComponentInteractionCreateEventArgs args) { return Task.CompletedTask; }
    Task ContextMenuInteractionCreated(DiscordClient sender, ContextMenuInteractionCreateEventArgs args) { return Task.CompletedTask; }
    Task DmChannelDeleted(DiscordClient sender, DmChannelDeleteEventArgs args) { return Task.CompletedTask; }
    Task GuildAvailable(DiscordClient sender, GuildCreateEventArgs args) { return Task.CompletedTask; }
    Task GuildBanAdded(DiscordClient sender, GuildBanAddEventArgs args) { return Task.CompletedTask; }
    Task GuildBanRemoved(DiscordClient sender, GuildBanRemoveEventArgs args) { return Task.CompletedTask; }
    Task GuildCreated(DiscordClient sender, GuildCreateEventArgs args) { return Task.CompletedTask; }
    Task GuildDeleted(DiscordClient sender, GuildDeleteEventArgs args) { return Task.CompletedTask; }
    Task GuildDownloadCompleted(DiscordClient sender, GuildDownloadCompletedEventArgs args) { return Task.CompletedTask; }
    Task GuildEmojisUpdated(DiscordClient sender, GuildEmojisUpdateEventArgs args) { return Task.CompletedTask; }
    Task GuildIntegrationsUpdated(DiscordClient sender, GuildIntegrationsUpdateEventArgs args) { return Task.CompletedTask; }
    Task GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs args) { return Task.CompletedTask; }
    Task GuildMemberRemoved(DiscordClient sender, GuildMemberRemoveEventArgs args) { return Task.CompletedTask; }
    Task GuildMembersChunked(DiscordClient sender, GuildMembersChunkEventArgs args) { return Task.CompletedTask; }
    Task GuildMemberUpdated(DiscordClient sender, GuildMemberUpdateEventArgs args) { return Task.CompletedTask; }
    Task GuildRoleCreated(DiscordClient sender, GuildRoleCreateEventArgs args) { return Task.CompletedTask; }
    Task GuildRoleDeleted(DiscordClient sender, GuildRoleDeleteEventArgs args) { return Task.CompletedTask; }
    Task GuildRoleUpdated(DiscordClient sender, GuildRoleUpdateEventArgs args) { return Task.CompletedTask; }
    Task GuildStickersUpdated(DiscordClient sender, GuildStickersUpdateEventArgs args) { return Task.CompletedTask; }
    Task GuildUnavailable(DiscordClient sender, GuildDeleteEventArgs args) { return Task.CompletedTask; }
    Task GuildUpdated(DiscordClient sender, GuildUpdateEventArgs args) { return Task.CompletedTask; }
    Task Heartbeated(DiscordClient sender, HeartbeatEventArgs args) { return Task.CompletedTask; }
    Task IntegrationCreated(DiscordClient sender, IntegrationCreateEventArgs args) { return Task.CompletedTask; }
    Task IntegrationDeleted(DiscordClient sender, IntegrationDeleteEventArgs args) { return Task.CompletedTask; }
    Task IntegrationUpdated(DiscordClient sender, IntegrationUpdateEventArgs args) { return Task.CompletedTask; }
    Task InteractionCreated(DiscordClient sender, InteractionCreateEventArgs args) { return Task.CompletedTask; }
    Task InviteCreated(DiscordClient sender, InviteCreateEventArgs args) { return Task.CompletedTask; }
    Task InviteDeleted(DiscordClient sender, InviteDeleteEventArgs args) { return Task.CompletedTask; }
    Task MessageCreated(DiscordClient sender, MessageCreateEventArgs args) { return Task.CompletedTask; }
    Task MessageDeleted(DiscordClient sender, MessageDeleteEventArgs args) { return Task.CompletedTask; }
    Task MessageReactionAdded(DiscordClient sender, MessageReactionAddEventArgs args) { return Task.CompletedTask; }
    Task MessageReactionRemoved(DiscordClient sender, MessageReactionRemoveEventArgs args) { return Task.CompletedTask; }
    Task MessageReactionRemovedEmoji(DiscordClient sender, MessageReactionRemoveEmojiEventArgs args) { return Task.CompletedTask; }
    Task MessageReactionsCleared(DiscordClient sender, MessageReactionsClearEventArgs args) { return Task.CompletedTask; }
    Task MessagesBulkDeleted(DiscordClient sender, MessageBulkDeleteEventArgs args) { return Task.CompletedTask; }
    Task MessageUpdated(DiscordClient sender, MessageUpdateEventArgs args) { return Task.CompletedTask; }
    Task ModalSubmitted(DiscordClient sender, ModalSubmitEventArgs args) { return Task.CompletedTask; }
    Task PresenceUpdated(DiscordClient sender, PresenceUpdateEventArgs args) { return Task.CompletedTask; }
    Task Ready(DiscordClient sender, ReadyEventArgs args) { return Task.CompletedTask; }
    Task Resumed(DiscordClient sender, ReadyEventArgs args) { return Task.CompletedTask; }
    Task SocketClosed(DiscordClient sender, SocketCloseEventArgs args) { return Task.CompletedTask; }
    Task SocketErrored(DiscordClient sender, SocketErrorEventArgs args) { return Task.CompletedTask; }
    Task SocketOpened(DiscordClient sender, SocketEventArgs args) { return Task.CompletedTask; }
    Task StageInstanceCreated(DiscordClient sender, StageInstanceCreateEventArgs args) { return Task.CompletedTask; }
    Task StageInstanceDeleted(DiscordClient sender, StageInstanceDeleteEventArgs args) { return Task.CompletedTask; }
    Task StageInstanceUpdated(DiscordClient sender, StageInstanceUpdateEventArgs args) { return Task.CompletedTask; }
    Task ThreadCreated(DiscordClient sender, ThreadCreateEventArgs args) { return Task.CompletedTask; }
    Task ThreadDeleted(DiscordClient sender, ThreadDeleteEventArgs args) { return Task.CompletedTask; }
    Task ThreadListSynced(DiscordClient sender, ThreadListSyncEventArgs args) { return Task.CompletedTask; }
    Task ThreadMembersUpdated(DiscordClient sender, ThreadMembersUpdateEventArgs args) { return Task.CompletedTask; }
    Task ThreadMemberUpdated(DiscordClient sender, ThreadMemberUpdateEventArgs args) { return Task.CompletedTask; }
    Task ThreadUpdated(DiscordClient sender, ThreadUpdateEventArgs args) { return Task.CompletedTask; }
    Task TypingStarted(DiscordClient sender, TypingStartEventArgs args) { return Task.CompletedTask; }
    Task UnknownEvent(DiscordClient sender, UnknownEventArgs args) { return Task.CompletedTask; }
    Task UserSettingsUpdated(DiscordClient sender, UserSettingsUpdateEventArgs args) { return Task.CompletedTask; }
    Task UserUpdated(DiscordClient sender, UserUpdateEventArgs args) { return Task.CompletedTask; }
    Task VoiceServerUpdated(DiscordClient sender, VoiceServerUpdateEventArgs args) { return Task.CompletedTask; }
    Task VoiceStateUpdated(DiscordClient sender, VoiceStateUpdateEventArgs args) { return Task.CompletedTask; }
    Task WebhooksUpdated(DiscordClient sender, WebhooksUpdateEventArgs args) { return Task.CompletedTask; }
    Task Zombied(DiscordClient sender, ZombiedEventArgs args) { return Task.CompletedTask; }
}
