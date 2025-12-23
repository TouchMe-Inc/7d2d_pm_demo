using PluginManager.Api;
using PluginManager.Api.Capabilities.Implementations.Events.GameEvents;
using PluginManager.Api.Capabilities.Implementations.Utils;
using PluginManager.Api.Contracts;
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
        RegisterEventHandler<TileEntityAccessAttemptEvent>(OnTileEntityAccessAttempt, HookMode.Pre);
        RegisterEventHandler<EntityDamageEvent>(OnEntityDamage, HookMode.Pre);
    }

    protected override void OnUnload()
    {
    }

    private HookResult OnTileEntityAccessAttempt(TileEntityAccessAttemptEvent evt)
    {
        var claimStatus = Capabilities.Get<IPlayerUtil>().GetClaimOwner(evt.EntityId, evt.TileEntityPosition);

        if (claimStatus == LandClaimOwner.None)
        {
            return HookResult.Continue;
        }
        
        return claimStatus == LandClaimOwner.Other ? HookResult.Handled : HookResult.Continue;
    }
    
    private HookResult OnEntityDamage(EntityDamageEvent evt)
    {
        Log.Out($"OnEntityDamage {evt.VictimEntityId} << {evt.AttackerEntityId} take {evt.Strength}");

        evt.Strength = 1;
        return HookResult.Changed;
    }
}