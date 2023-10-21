using Newtonsoft.Json;

namespace HellBotLib.IO;

/// <summary>
/// Utility class for managing and storing configuration data for a bot, guilds, and users.
/// </summary>
public static class ConfigManager
{
    /// <summary>
    /// Validates the required directory structure for configuration storage.
    /// </summary>
    private static void ValidateFolders()
    {
        Directory.CreateDirectory("Config");
        Directory.CreateDirectory("Config/Guild");
        Directory.CreateDirectory("Config/User");
    }

    /// <summary>
    /// Stores a bot configuration.
    /// </summary>
    /// <typeparam name="T">The type of bot configuration to store.</typeparam>
    /// <param name="config">The bot configuration to store.</param>
    public static void StoreBot<T>(T config) where T : class, IBotConfig
    {
        ValidateFolders();

        File.WriteAllText($"Config/{typeof(T).Name}.json", JsonConvert.SerializeObject(config));
    }

    /// <summary>
    /// Stores a guild-specific configuration.
    /// </summary>
    /// <typeparam name="T">The type of guild configuration to store.</typeparam>
    /// <param name="guild">The ID of the guild associated with the configuration.</param>
    /// <param name="config">The guild configuration to store.</param>
    public static void StoreGuild<T>(ulong guild, T config) where T : class, IGuildConfig
    {
        ValidateFolders();

        File.WriteAllText($"Config/Guild/{guild}/{typeof(T).Name}.json", JsonConvert.SerializeObject(config));
    }

    /// <summary>
    /// Stores a user-specific configuration.
    /// </summary>
    /// <typeparam name="T">The type of user configuration to store.</typeparam>
    /// <param name="user">The ID of the user associated with the configuration.</param>
    /// <param name="config">The user configuration to store.</param>
    public static void StoreUser<T>(ulong user, T config) where T : class, IUserConfig
    {
        ValidateFolders();

        File.WriteAllText($"Config/User/{user}/{typeof(T).Name}.json", JsonConvert.SerializeObject(config));
    }

    /// <summary>
    /// Retrieves a bot configuration.
    /// </summary>
    /// <typeparam name="T">The type of bot configuration to retrieve.</typeparam>
    /// <returns>The bot configuration or a default configuration if not found.</returns>
    public static T? GetBot<T>() where T : class, IBotConfig
    {
        ValidateFolders();

        string filePath = $"Config/{typeof(T).Name}.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            // Handle the case where the configuration file doesn't exist, e.g., create a default configuration.
            return null; // You may want to return a default config or null as needed.
        }
    }

    /// <summary>
    /// Retrieves a guild-specific configuration.
    /// </summary>
    /// <typeparam name="T">The type of guild configuration to retrieve.</typeparam>
    /// <param name="guild">The ID of the guild associated with the configuration.</param>
    /// <returns>The guild configuration or a default configuration if not found.</returns>
    public static T? GetGuild<T>(ulong guild) where T : class, IGuildConfig
    {
        ValidateFolders();

        string filePath = $"Config/Guild/{guild}/{typeof(T).Name}.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            // Handle the case where the configuration file doesn't exist, e.g., create a default configuration.
            return null; // You may want to return a default config or null as needed.
        }
    }


    /// <summary>
    /// Retrieves a user-specific configuration.
    /// </summary>
    /// <typeparam name="T">The type of user configuration to retrieve.</typeparam>
    /// <param name="user">The ID of the user associated with the configuration.</param>
    /// <returns>The user configuration or a default configuration if not found.</returns>
    public static T? GetUser<T>(ulong user) where T : class, IUserConfig
    {
        ValidateFolders();

        string filePath = $"Config/User/{user}/{typeof(T).Name}.json";

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            // Handle the case where the configuration file doesn't exist, e.g., create a default configuration.
            return null; // You may want to return a default config or null as needed.
        }
    }

    /// <summary>
    /// Validates the required directory structure for configuration storage asynchronously.
    /// </summary>
    private static async Task ValidateFoldersAsync()
    {
        await Task.Run(() =>
        {
            Directory.CreateDirectory("Config");
            Directory.CreateDirectory("Config/Guild");
            Directory.CreateDirectory("Config/User");
        });
    }

    /// <summary>
    /// Stores a bot configuration asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of bot configuration to store.</typeparam>
    /// <param name="config">The bot configuration to store.</param>
    public static async Task StoreBotAsync<T>(T config) where T : class, IBotConfig
    {
        await ValidateFoldersAsync();

        await File.WriteAllTextAsync($"Config/{typeof(T).Name}.json", JsonConvert.SerializeObject(config));
    }

    /// <summary>
    /// Stores a guild-specific configuration asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of guild configuration to store.</typeparam>
    /// <param name="guild">The ID of the guild associated with the configuration.</param>
    /// <param name="config">The guild configuration to store.</param>
    public static async Task StoreGuildAsync<T>(ulong guild, T config) where T : class, IGuildConfig
    {
        await ValidateFoldersAsync();

        await File.WriteAllTextAsync($"Config/Guild/{guild}/{typeof(T).Name}.json", JsonConvert.SerializeObject(config));
    }

    /// <summary>
    /// Stores a user-specific configuration asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of user configuration to store.</typeparam>
    /// <param name="user">The ID of the user associated with the configuration.</param>
    /// <param name="config">The user configuration to store.</param>
    public static async Task StoreUserAsync<T>(ulong user, T config) where T : class, IUserConfig
    {
        await ValidateFoldersAsync();

        await File.WriteAllTextAsync($"Config/User/{user}/{typeof(T).Name}.json", JsonConvert.SerializeObject(config));
    }

    /// <summary>
    /// Retrieves a bot configuration asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of bot configuration to retrieve.</typeparam>
    /// <returns>The bot configuration or a default configuration if not found.</returns>
    public static async Task<T?> GetBotAsync<T>() where T : class, IBotConfig
    {
        await ValidateFoldersAsync();

        string filePath = $"Config/{typeof(T).Name}.json";

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Retrieves a guild-specific configuration asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of guild configuration to retrieve.</typeparam>
    /// <param name="guild">The ID of the guild associated with the configuration.</param>
    /// <returns>The guild configuration or a default configuration if not found.</returns>
    public static async Task<T?> GetGuildAsync<T>(ulong guild) where T : class, IGuildConfig
    {
        await ValidateFoldersAsync();

        string filePath = $"Config/Guild/{guild}/{typeof(T).Name}.json";

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Retrieves a user-specific configuration asynchronously.
    /// </summary>
    /// <typeparam name="T">The type of user configuration to retrieve.</typeparam>
    /// <param name="user">The ID of the user associated with the configuration.</param>
    /// <returns>The user configuration or a default configuration if not found.</returns>
    public static async Task<T?> GetUserAsync<T>(ulong user) where T : class, IUserConfig
    {
        await ValidateFoldersAsync();

        string filePath = $"Config/User/{user}/{typeof(T).Name}.json";

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            return null;
        }
    }
}
