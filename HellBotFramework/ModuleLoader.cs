using DSharpPlus.ModalCommands;
using DSharpPlus.SlashCommands;
using HellBotLib;
using Serilog;
using System.Reflection;

internal static class ModuleLoader
{
    public static bool LoadModule(this SlashCommandsExtension slash, string path)
    {
        var assembly = Assembly.LoadFrom(path);

        IModule? module = FindModule(assembly);

        if (module != null)
        {
            Log.Information("Module {ModuleName}", module.Name);
            Log.Information("Description: {ModuleDescription}", module.Description);

            slash.RegisterCommands(assembly);
            return true;
        }
        Log.Warning("The dll at '{Path}' is not a valid module, or contains more than 1 module descriptor. Go check the documentation for more info. Treating it as a library instead for now, if this is a mistake then fix it and try again.", path);
        return false;
    }

    public static bool LoadModule(this ModalCommandsExtension modal, string path)
    {
        var assembly = Assembly.LoadFrom(path);

        IModule? module = FindModule(assembly);

        if (module != null)
        {
            modal.RegisterModals(assembly);
            return true;
        }
        Log.Information("The dll at '{Path}' is not a valid module, or contains more than 1 module descriptor. Go check the documentation for more info. Treating it as a library instead for now, if this is a mistake then fix it and try again.");
        return false;
    }

    private static IModule? FindModule(Assembly assembly)
    {
        var moduleTypes = assembly.GetTypes()
            .Where(type => typeof(IModule).IsAssignableFrom(type) && 
                !type.IsInterface && 
                !type.IsAbstract)
            .ToList();

        if (moduleTypes.Count == 0 || moduleTypes.Count > 1)
        {
            return null;
        }

        var type = moduleTypes.First();

        var instance = Activator.CreateInstance(type);

        return instance as IModule;
    }
}
