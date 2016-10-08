public class LidUpgradeManager : WeaponUpgradeManager<LidDamageUpgrade, LidReloadTimeUpgrade, LidClipSizeUpgrade>
{
    public override UpgradeHelper<LidClipSizeUpgrade> ClipSize { get { return LidClipSizeUpgrade.UpgradeManager; } }
    public override UpgradeHelper<LidDamageUpgrade> Damage { get { return LidDamageUpgrade.UpgradeManager; } }
    public override UpgradeHelper<LidReloadTimeUpgrade> ReloadTime { get { return LidReloadTimeUpgrade.UpgradeManager; } }
}
