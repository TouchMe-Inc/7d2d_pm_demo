using PluginManager.Api;
using PluginManager.Api.Events.GameEvents;
using PluginManager.Api.Hooks;

namespace DemoPlugin;

public class DemoPlugin : BasePlugin
{
    public override string ModuleName => "DemoPlugin";
    public override string ModuleVersion => "1.0.1";
    public override string ModuleAuthor => "TouchMe-Inc";
    public override string ModuleDescription => "Demo plugin with command !hello";

    protected override void OnLoad()
    {
        Log.Out($"Plugin {ModuleName} is loaded");
        RegisterEventHandler<ChatMessageEvent>(OnChatMessage, HookMode.Pre);
    }

    protected override void OnUnload()
    {
        RegisterEventHandler<ChatMessageEvent>(OnChatMessage, HookMode.Pre);
        Log.Out($"Plugin {ModuleName} is unloaded");
    }

    private HookResult OnChatMessage(ChatMessageEvent evt)
    {
        if (evt.Message.StartsWith("!help"))
        {
            Log.Out($"Found trigger !help");
            return HookResult.Handled;
        }

        evt.Message = "[00ff00]" + evt.Message;
        evt.BBMode = GeneratedTextManager.BbCodeSupportMode.Supported;

        return HookResult.Changed;
    }
}