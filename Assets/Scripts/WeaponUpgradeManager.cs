
public abstract class WeaponUpgradeManager<DamageUpgrade, ReloadTimeUpgrade, ClipSizeUpgrade> : UpgradeManager
    where DamageUpgrade : Upgrade
    where ReloadTimeUpgrade : Upgrade
    where ClipSizeUpgrade : Upgrade
{
    public delegate void SteamUpgradeNotificationHandler<T>(T upgrade) where T : Upgrade;

    public event SteamUpgradeNotificationHandler<DamageUpgrade> DamageUpgraded;
    public event SteamUpgradeNotificationHandler<ReloadTimeUpgrade> ReloadTimeUpgraded;
    public event SteamUpgradeNotificationHandler<ClipSizeUpgrade> ClipSizeUpgraded;

    public abstract UpgradeHelper<DamageUpgrade> DamageUpgradeManager { get; }
    public abstract UpgradeHelper<ReloadTimeUpgrade> ReloadTimeUpgradeManager { get; }
    public abstract UpgradeHelper<ClipSizeUpgrade> ClipSizeUpgradeManager { get; }
    

    protected override void ConfigureUpgrades()
    {
        ConfigureUpgrade(_stats[0], DamageUpgradeManager, (upgrade) => { if (DamageUpgraded != null) DamageUpgraded(upgrade); });
        ConfigureUpgrade(_stats[1], ReloadTimeUpgradeManager, (upgrade) => { if (ReloadTimeUpgraded != null) ReloadTimeUpgraded(upgrade); });
        ConfigureUpgrade(_stats[2], ClipSizeUpgradeManager, (upgrade) => { if (ClipSizeUpgraded != null) ClipSizeUpgraded(upgrade); });
    }
}