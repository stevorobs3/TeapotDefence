
public class SteamUpgradeManager : WeaponUpgradeManager<SteamDamageUpgrade, SteamReloadTimeUpgrade, SteamClipSizeUpgrade>
{
    public override UpgradeHelper<SteamClipSizeUpgrade> ClipSizeUpgradeManager { get { return SteamClipSizeUpgrade.UpgradeManager; } }
    public override UpgradeHelper<SteamDamageUpgrade> DamageUpgradeManager { get { return SteamDamageUpgrade.UpgradeManager;} }
    public override UpgradeHelper<SteamReloadTimeUpgrade> ReloadTimeUpgradeManager { get { return SteamReloadTimeUpgrade.UpgradeManager; } }
}
