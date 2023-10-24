using Newtonsoft.Json;
using System;

namespace HellBotLib.IO;

/// <summary>
/// Utility class for managing and storing configuration data for a bot, guilds, and users.
/// </summary>
public static class ConfigManager
{
    private static readonly string LockFileExtension = ".lock";

    private static async Task LockFileAsync(string filePath)
    {
        var lockFilePath = filePath + LockFileExtension;
        while (File.Exists(lockFilePath))
        {
            await Task.Delay(100); // Wait and check again if the lock file is still present
        }
        File.Create(lockFilePath).Dispose();
    }

    private static void UnlockFile(string filePath)
    {
        var lockFilePath = filePath + LockFileExtension;
        if (File.Exists(lockFilePath))
        {
            File.Delete(lockFilePath);
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

        await LockFileAsync($"Config/{typeof(T).Name}.json");

        await File.WriteAllTextAsync($"Config/{typeof(T).Name}.json", JsonConvert.SerializeObject(config, Formatting.Indented));

        UnlockFile($"Config/{typeof(T).Name}.json");
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

        Directory.CreateDirectory($"Config/Guild/{guild}");

        await LockFileAsync($"Config/Guild/{guild}/{typeof(T).Name}.json");

        await File.WriteAllTextAsync($"Config/Guild/{guild}/{typeof(T).Name}.json", JsonConvert.SerializeObject(config, Formatting.Indented));

        UnlockFile($"Config/Guild/{guild}/{typeof(T).Name}.json");
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

        Directory.CreateDirectory($"Config/User/{user}");

        await LockFileAsync($"Config/User/{user}/{typeof(T).Name}.json");

        await File.WriteAllTextAsync($"Config/User/{user}/{typeof(T).Name}.json", JsonConvert.SerializeObject(config, Formatting.Indented));

        UnlockFile($"Config/User/{user}/{typeof(T).Name}.json");
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

        await LockFileAsync(filePath);

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            UnlockFile(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            UnlockFile(filePath);
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

        await LockFileAsync(filePath);

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            UnlockFile(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            UnlockFile(filePath);
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

        await LockFileAsync(filePath);

        if (File.Exists(filePath))
        {
            string json = await File.ReadAllTextAsync(filePath);
            UnlockFile(filePath);
            return JsonConvert.DeserializeObject<T>(json);
        }
        else
        {
            UnlockFile(filePath);
            return null;
        }
    }
}
