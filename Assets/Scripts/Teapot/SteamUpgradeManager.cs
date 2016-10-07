
public class SteamUpgradeManager : WeaponUpgradeManager<SteamDamageUpgrade, SteamReloadTimeUpgrade, SteamClipSizeUpgrade>
{
    public override UpgradeHelper<SteamClipSizeUpgrade> ClipSize { get { return SteamClipSizeUpgrade.UpgradeManager; } }
    public override UpgradeHelper<SteamDamageUpgrade> Damage { get { return SteamDamageUpgrade.UpgradeManager;} }
    public override UpgradeHelper<SteamReloadTimeUpgrade> ReloadTime { get { return SteamReloadTimeUpgrade.UpgradeManager; } }
}
