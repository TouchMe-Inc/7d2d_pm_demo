using PluginManager.Api;
using PluginManager.Api.Capabilities;
using PluginManager.Api.Exposed.Events;
using PluginManager.Api.Exposed.Events.GameEvents;
using PluginManager.Api.Hooks;

namespace DemoPlugin;

public class DemoPlugin : IPlugin
{
    public string ModuleName => "DemoPlugin";
    public string ModuleVersion => "1.0.0";
    public string ModuleAuthor => "TouchMe-Inc";
    public string ModuleDescription => "Demo plugin with command !hello";
    public string ModulePath { get; set; }

    public void Load(ICapabilityRegistry registry)
    {
        var events = registry.Resolve(new PluginCapability<IEventBus>("Events"));
        
        events.RegisterEventHandler<ChatMessageEvent>(OnChatMessage);
    }

    public void Unload(ICapabilityRegistry registry)
    {
        var events = registry.Resolve(new PluginCapability<IEventBus>("Events"));
        
        events.DeregisterEventHandler<ChatMessageEvent>(OnChatMessage);
    }
    public ModEvents.EModEventResult ChatMessage(ref ModEvents.SChatMessageData _data) { // ... }
    private HookResult OnChatMessage(ChatMessageEvent evt)
    {
        Log.Out($"[{ModuleName}] Hooked {evt.EventName}");
        // if (string.IsNullOrEmpty(message) || clientInfo == null) return ModEvents.EModEventResult.Continue;
        //
        // if (message.StartsWith("!") && message == "!hello")
        // {
        //     return ModEvents.EModEventResult.StopHandlersAndVanilla;
        // }

        return HookResult.Continue;
    }
}