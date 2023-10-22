using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using HellBotLib;
using HellBotLib.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

// Configure the Serilog logger for logging to the console and a log file
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt")
    .CreateLogger();

// Load the bot configuration from the file asynchronously
var config = await ConfigManager.GetBotAsync<BotConfig>();

// Check if the bot configuration file doesn't exist
if (config is null)
{
    // Create a temporary bot configuration to use as a fallback
    config = new BotConfig();

    // Log an informational message indicating that the configuration is missing and a new one is being generated
    Log.Information("Failed to retrieve the bot configuration. A new configuration is being generated. Make sure to configure it all and try again.");

    // Store the temporary bot configuration in the file
    await ConfigManager.StoreBotAsync(config);

    // Exit or return, possibly indicating that further configuration is needed
    return;
}

// Create a logger factory using Serilog
var loggerFactory = new LoggerFactory().AddSerilog();

// Create a service provider with a singleton Random service
var services = new ServiceCollection()
    .AddSingleton<Random>()
    .BuildServiceProvider();

var intentBuilder = DiscordIntents.AllUnprivileged;

if (config.PresenceIntent)
    intentBuilder |= DiscordIntents.GuildPresences;

if (config.ServerMembersIntent)
    intentBuilder |= DiscordIntents.GuildMembers;

if (config.MessageContentIntent)
    intentBuilder |= DiscordIntents.MessageContents;

if (config.PresenceIntent && config.MessageContentIntent && config.ServerMembersIntent)
    intentBuilder = DiscordIntents.All;

// Configure the Discord client
var conf = new DiscordConfiguration()
{
    LoggerFactory = loggerFactory,
    Token = config.Token,
    Intents = intentBuilder,
    LogUnknownEvents = false
};

// Create a new sharded Discord client
var client = new DiscordShardedClient(conf);

// Configure the interactivity for the client
var interConf = new InteractivityConfiguration()
{
    PollBehaviour = PollBehaviour.DeleteEmojis,
    Timeout = TimeSpan.FromMinutes(2),
    ButtonBehavior = ButtonPaginationBehavior.DeleteButtons,
};

await client.UseInteractivityAsync(interConf);

// Configure the SlashCommands for the client
var slashConf = new SlashCommandsConfiguration()
{
    Services = services
};

// Enable SlashCommands on the client
var slash = await client.UseSlashCommandsAsync(slashConf);

// Create a 'Modules' directory if it doesn't exist
Directory.CreateDirectory("Modules");

// Load the modules configuration
var modules = await ConfigManager.GetBotAsync<Modules>() ?? new Modules();

// Initialize a list to keep track of modules to remove
List<string> modulesToRemove = new();

// Check and record modules that should be removed
foreach (var module in modules.ModuleList.Keys)
{
    if (File.Exists(module)) continue;
    modulesToRemove.Add(module);
}

// Remove the modules that no longer exist
foreach (var module in modulesToRemove)
{
    modules.ModuleList.Remove(module);
}

List<string> librariesToRemove = new();

foreach (var library in modules.Libraries)
{
    if (File.Exists(library)) continue;
    librariesToRemove.Add(library);
}

foreach (var library in librariesToRemove)
{
    modules.Libraries.Remove(library);
}

// Get a list of module file paths in the 'Modules' directory
string[] modulePaths = Directory.GetFiles("Modules", "*.dll", SearchOption.TopDirectoryOnly);

// Add new modules to the list of modules
foreach (string modulePath in modulePaths)
{
    if (modules.ModuleList.ContainsKey(modulePath) || modules.Libraries.Contains(modulePath)) continue;

    // Add the module to the list and set it to be enabled
    modules.ModuleList.Add(modulePath, true);
}

List<string> modulesToMove = new();

// Register and load commands for each SlashCommand module
foreach (var s in slash.Values)
{
    s.RegisterCommands<Core>();

    foreach (var module in modules.ModuleList)
    {
        if (module.Value)
        {
            if (s.LoadModule(module.Key) is false)
            {
                // This is actually a plugin, not a module
                modulesToMove.Add(module.Key);
            }
        }
    }
}

foreach (var module in modulesToMove)
{
    modules.ModuleList.Remove(module);
    modules.Libraries.Add(module);
}

client.Ready += Client_Ready;

async Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
{
    if (config.DefaultPresence.Length == 0) return;

    await client.UpdateStatusAsync(new DiscordActivity(config.DefaultPresence, ActivityType.Playing), UserStatus.Online);
}

foreach (var module in modules.ModuleList.Keys)
{
    EventRegister.RegisterEventsForModule(ref client, module);
}

// Store the updated modules configuration
await ConfigManager.StoreBotAsync(modules);

await client.StartAsync();

while (Core.KeepRunning) { await Task.Delay(1000); }
