
public class TeapotUpgradeManager : UpgradeManager
{
    public delegate void TeapotUpgradeNotificationHandler<T>(T upgrade);
    public event TeapotUpgradeNotificationHandler<TeapotRangeUpgrade> RangeUpgraded;
    public event TeapotUpgradeNotificationHandler<TeapotDPSUpgrade>   DPSUpgraded;
    public event TeapotUpgradeNotificationHandler<TeapotSpeedUpgrade> SpeedUpgraded;


    new void Start()
    {
        _dpsUpgrade = new UpgradeHelper<TeapotDPSUpgrade>(TeapotDPSUpgrade.Upgrades);
        _rangeUpgrade = new UpgradeHelper<TeapotRangeUpgrade>(TeapotRangeUpgrade.Upgrades);
        _speedUpgrade = new UpgradeHelper<TeapotSpeedUpgrade>(TeapotSpeedUpgrade.Upgrades);

        base.Start();
    }

    private UpgradeHelper<TeapotDPSUpgrade> _dpsUpgrade;
    private UpgradeHelper<TeapotRangeUpgrade> _rangeUpgrade;
    private UpgradeHelper<TeapotSpeedUpgrade> _speedUpgrade;

    public UpgradeHelper<TeapotDPSUpgrade> DPSUpgradeManager {  get { return _dpsUpgrade; } }
    public UpgradeHelper<TeapotRangeUpgrade> RangeUpgradeManager { get { return _rangeUpgrade; } }
    public UpgradeHelper<TeapotSpeedUpgrade> SpeedUpgradeManager { get { return _speedUpgrade; } }

    protected override void ConfigureUpgrades()
    {
        ConfigureUpgrade(_stats[0], _dpsUpgrade, (upgrade) => { if (DPSUpgraded != null) DPSUpgraded(upgrade); });
        ConfigureUpgrade(_stats[1], _rangeUpgrade, (upgrade) => { if (RangeUpgraded != null) RangeUpgraded(upgrade); });
        ConfigureUpgrade(_stats[2], _speedUpgrade, (upgrade) => { if (SpeedUpgraded != null) SpeedUpgraded(upgrade); });
    }
}