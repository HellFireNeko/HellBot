using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using HellBotLib.IO;

internal class Core : ApplicationCommandModule
{
    public static bool KeepRunning = true;

    [SlashCommand("shutdown", "Stops the bot from taking over the world")]
    [SlashRequireOwner]
    public async Task Shutdown(InteractionContext ctx)
    {
        await ctx.CreateResponseAsync("Goodbye cruel world", true);

        KeepRunning = false;

        await ctx.Client.DisconnectAsync();
    }

    [SlashCommand("Ping", "Gets the delay of the bot!")]
    public async Task Ping(InteractionContext ctx)
    {
        await ctx.DeferAsync();

        var ping = ctx.Client.Ping;

        await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Pong! {ping}ms"));
    }

    internal sealed class ModulesAutocomplete : IAutocompleteProvider
    {
        public async Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
        {
            var modules = await ConfigManager.GetBotAsync<Modules>() ?? throw new ArgumentNullException("FATAL ERROR THE MODULES IS NOT DEFINED");

            var choices = new List<DiscordAutoCompleteChoice>();

            foreach (var module in modules.ModuleList.Keys)
            {
                choices.Add(new DiscordAutoCompleteChoice(Path.GetFileName(module), module));
            }

            return choices;
        }
    }

    [SlashCommandGroup("Module", "Manage modules")]
    [SlashRequireOwner]
    public class ModulesGroup : ApplicationCommandModule
    {
        [SlashCommand("Enable", "Enables a module, requires restart")]
        [SlashRequireOwner]
        public async Task Enable(InteractionContext ctx, [Autocomplete(typeof(ModulesAutocomplete)), Option("Module", "The module to enable")] string module)
        {
            await ctx.DeferAsync(true);

            var modules = await ConfigManager.GetBotAsync<Modules>() ?? throw new ArgumentNullException("FATAL ERROR THE MODULES IS NOT DEFINED");

            if (modules.ModuleList.ContainsKey(module))
            {
                modules.ModuleList[module] = true;

                await ConfigManager.StoreBotAsync(modules);

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Enabled the module, please restart the bot now!"));
                return;
            }
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("That module does not exist in the folder!"));
        }

        [SlashCommand("Disable", "Disables a module, requires restart")]
        [SlashRequireOwner]
        public async Task Disable(InteractionContext ctx, [Autocomplete(typeof(ModulesAutocomplete)), Option("Module", "The module to disable")] string module)
        {
            await ctx.DeferAsync(true);

            var modules = await ConfigManager.GetBotAsync<Modules>() ?? throw new ArgumentNullException("FATAL ERROR THE MODULES IS NOT DEFINED");

            if (modules.ModuleList.ContainsKey(module))
            {
                modules.ModuleList[module] = false;

                await ConfigManager.StoreBotAsync(modules);

                await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("Disabled the module, please restart the bot now!"));
                return;
            }
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("That module does not exist in the folder!"));
        }

        [SlashCommand("Scan", "Scans for new modules, requires restart")]
        [SlashRequireOwner]
        public async Task Scan(InteractionContext ctx)
        {
            await ctx.DeferAsync(true);

            var modules = await ConfigManager.GetBotAsync<Modules>() ?? throw new ArgumentNullException("FATAL ERROR THE MODULES IS NOT DEFINED");

            // Get a list of module file paths in the 'Modules' directory
            string[] modulePaths = Directory.GetFiles("Modules", "*.dll", SearchOption.TopDirectoryOnly);

            int foundModules = 0;

            // Add new modules to the list of modules
            foreach (string modulePath in modulePaths)
            {
                if (modules.ModuleList.ContainsKey(modulePath) || modules.Libraries.Contains(modulePath)) continue;

                // Add the module to the list and set it to be enabled
                modules.ModuleList.Add(modulePath, true);
                foundModules++;
            }

            await ConfigManager.StoreBotAsync(modules);
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent($"Found {foundModules} new modules! Please restart to have them take effect!"));
        }
    }
}
