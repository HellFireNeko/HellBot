using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;
using HellBotLib.IO;

[SlashCommandGroup("Core", "Core commands, only the owner can use these!")]
[SlashRequireOwner]
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


    internal sealed class ModulesAutocomplete : IAutocompleteProvider
    {
        public async Task<IEnumerable<DiscordAutoCompleteChoice>> Provider(AutocompleteContext ctx)
        {
            var modules = await ConfigManager.GetBotAsync<Modules>() ?? throw new ArgumentNullException("FATAL ERROR THE MODULES IS NOT DEFINED");

            // Get a list of module file paths in the 'Modules' directory
            string[] modulePaths = Directory.GetFiles("Modules", "*.dll", SearchOption.TopDirectoryOnly);

            // Add new modules to the list of modules
            foreach (string modulePath in modulePaths)
            {
                if (modules.ModuleList.ContainsKey(modulePath) || modules.Libraries.Contains(modulePath)) continue;

                // Add the module to the list and set it to be enabled
                modules.ModuleList.Add(modulePath, true);
            }

            await ConfigManager.StoreBotAsync(modules);

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

            }
            await ctx.EditResponseAsync(new DiscordWebhookBuilder().WithContent("That module does not exist in the folder!"));
        }
    }
}
