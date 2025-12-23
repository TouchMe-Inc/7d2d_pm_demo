using PluginManager.Api;
using PluginManager.Api.Capabilities.Implementations.Commands;
using PluginManager.Api.Capabilities.Implementations.Events.GameEvents;
using PluginManager.Api.Hooks;

namespace DemoPlugin;

public class DemoPlugin : BasePlugin
{
    public override string ModuleName => "DemoPlugin";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "TouchMe-Inc";
    public override string ModuleDescription => "Demo plugin";

    protected override void OnLoad()
    {
        RegisterEventHandler<PlayerJoinedGameEvent>(OnPlayerJoined, HookMode.Pre);
        RegisterEventHandler<TileEntityAccessAttemptEvent>(OnTileEntityAccessAttempt, HookMode.Pre);

        RegisterCommand("help", "Print demo help", OnTriggeredHelp);

        Log.Out($"Plugin {ModuleName} is loaded");
    }

    protected override void OnUnload()
    {
        Log.Out($"Plugin {ModuleName} is unloaded");
    }

    private HookResult OnPlayerJoined(PlayerJoinedGameEvent evt)
    {
        Log.Out($"{evt.EventName} : {evt.EntityId}");
        return HookResult.Continue;
    }

    private HookResult OnTileEntityAccessAttempt(TileEntityAccessAttemptEvent evt)
    {
        Log.Out($"{evt.EntityIdThatOpenedIt} : {evt.TileEntityType}");

        return HookResult.Continue;
    }

    private void OnTriggeredHelp(ICommandContext ctx) =>
        ChatMessenger.SendTo(ctx.ClientInfo.EntityId,
            $"[ffaaaa][{ModuleName}][-] Client {ctx.ClientInfo.Name} trigger help");
}