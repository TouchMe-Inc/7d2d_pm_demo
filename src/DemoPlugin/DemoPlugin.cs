using PluginManager.Api;

namespace DemoPlugin;

public class DemoPlugin : IPlugin
{
    public string ModuleName => "DemoPlugin";
    public string ModuleVersion => "1.0.0";
    public string ModuleAuthor => "TouchMe-Inc";
    public string ModuleDescription => "Demo plugin";
    public string ModulePath { get; set; }

    public void Load(bool hotReload)
    {
        Log.Out($"[{ModuleName}] Loaded (hotReload={hotReload}) from {ModulePath}");
    }

    public void Unload(bool hotReload)
    {
        Log.Out($"[{ModuleName}] Unloaded (hotReload={hotReload})");
    }
}