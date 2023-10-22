# HellBot

**HellBot** is a bot framework designed to streamline bot setup. It simplifies the process, so you can focus on creating modules or quickly use built-in ones.

## Table of Contents
- [Introduction](#hellbot)
- [How to Setup](#how-to-setup)
- [Creating a Module](#creating-a-module)
- [Possible Issues](#possible-issues)

## How to Setup

1. Start by downloading the latest release or source code.
2. Upon starting the bot for the first time, several files will be generated.
3. Open the newly created `BotConfig.json` file in the `Config` directory and input your token, a default presence (if desired), and any required intents.
4. Restart the bot; it will scan its `Modules` folder and load all `.dll` files.
5. The bot is now ready to use, but you may want to add modules to enhance its functionality.

## Creating a Module

Creating a HellBot module is straightforward:

1. Create a new class library project, preferably in `.Net 7` or above.
2. Add the required NuGet packages, including [DSharpPlus](https://github.com/DSharpPlus/DSharpPlus).
3. Reference the `HellBotLib.dll` in your project.

Now your project is ready to become a HellBotModule!

To start, create a module descriptor, where only one should exist in a project:

```csharp
using HellBotLib;

public class MyModuleDescriptor : IModule
{
    public string Name => "My amazing module!";
    public string Description => "It does some stuff!";
}
```
After adding a module descriptor, HellBot will recognize it and load any `ApplicationCommandModule` classes from the assembly. It also links events to any instance of `IClientEvents`.

To use the `IClientEvents` interface, inherit from it and define the event you need. There are numerous events available; refer to the interface source for details.

## Possible Issues
- "My event handler triggers, but I can't read the message content": This issue is due to your bot lacking the `Message Content` intent. Enable it on the Discord Developer Portal and set the `Message Content` intent to true in the `BotConfig.json`.