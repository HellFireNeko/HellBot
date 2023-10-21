namespace HellBotLib;
/// <summary>
/// Represents a module that can be loaded and managed by an application.
/// </summary>
public interface IModule
{
    /// <summary>
    /// Gets the name of the module.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets a brief description of the module.
    /// </summary>
    string Description { get; }
}

