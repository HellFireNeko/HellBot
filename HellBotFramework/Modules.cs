using HellBotLib;

internal class Modules : IBotConfig
{
    public Dictionary<string, bool> ModuleList = new();
    public List<string> Libraries = new();

    public Modules() { }
}
