
public abstract class WeaponUpgradeManager<DamageUpgrade, ReloadTimeUpgrade, ClipSizeUpgrade> : UpgradeManager
    where DamageUpgrade : Upgrade
    where ReloadTimeUpgrade : Upgrade
    where ClipSizeUpgrade : Upgrade
{
    public delegate void SteamUpgradeNotificationHandler<T>(T upgrade) where T : Upgrade;

    public event SteamUpgradeNotificationHandler<DamageUpgrade> DamageUpgraded;
    public event SteamUpgradeNotificationHandler<ReloadTimeUpgrade> ReloadTimeUpgraded;
    public event SteamUpgradeNotificationHandler<ClipSizeUpgrade> ClipSizeUpgraded;

    public abstract UpgradeHelper<DamageUpgrade> Damage { get; }
    public abstract UpgradeHelper<ReloadTimeUpgrade> ReloadTime { get; }
    public abstract UpgradeHelper<ClipSizeUpgrade> ClipSize { get; }
    
    void Awake()
    {
        Damage.Reset();
        ReloadTime.Reset();
        ClipSize.Reset();
    }

    protected override void ConfigureUpgrades()
    {
        ConfigureUpgrade(_stats[0], Damage, (upgrade) => { if (DamageUpgraded != null) DamageUpgraded(upgrade); });
        ConfigureUpgrade(_stats[1], ReloadTime, (upgrade) => { if (ReloadTimeUpgraded != null) ReloadTimeUpgraded(upgrade); });
        ConfigureUpgrade(_stats[2], ClipSize, (upgrade) => { if (ClipSizeUpgraded != null) ClipSizeUpgraded(upgrade); });
    }
}