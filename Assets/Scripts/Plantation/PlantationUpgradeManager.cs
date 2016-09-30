
public class PlantationUpgradeManager : UpgradeManager
{
    public event UpgradeNotificationHandler<PlantationRegenUpgrade> RegenUpgraded;
    public event UpgradeNotificationHandler<PlantationHealthUpgrade> HealthUpgraded;
    public event UpgradeNotificationHandler<PlantationLPSUpgrade> LPSUpgraded;
    public event UpgradeNotificationHandler<PlantationLeavesUpgrade> LeavesUpgraded;

    new void Start()
    {
        _regenUpgrade = new UpgradeHelper<PlantationRegenUpgrade>(PlantationRegenUpgrade.Upgrades);
        _healthUpgrade = new UpgradeHelper<PlantationHealthUpgrade>(PlantationHealthUpgrade.Upgrades);
        _LPSUpgrade = new UpgradeHelper<PlantationLPSUpgrade>(PlantationLPSUpgrade.Upgrades);
        _leavesUpgrade = new UpgradeHelper<PlantationLeavesUpgrade>(PlantationLeavesUpgrade.Upgrades);

        base.Start();
        if (RegenUpgraded != null)  RegenUpgraded(_regenUpgrade.Current);
        if (HealthUpgraded != null) HealthUpgraded(_healthUpgrade.Current);
        if (LPSUpgraded != null)    LPSUpgraded(_LPSUpgrade.Current);
        if (LeavesUpgraded != null) LeavesUpgraded(_leavesUpgrade.Current);
    }

    private UpgradeHelper<PlantationRegenUpgrade> _regenUpgrade;
    private UpgradeHelper<PlantationHealthUpgrade> _healthUpgrade;
    private UpgradeHelper<PlantationLPSUpgrade> _LPSUpgrade;
    private UpgradeHelper<PlantationLeavesUpgrade> _leavesUpgrade;

    public UpgradeHelper<PlantationRegenUpgrade> DPSUpgradeManager {  get { return _regenUpgrade; } }
    public UpgradeHelper<PlantationHealthUpgrade> RangeUpgradeManager { get { return _healthUpgrade; } }
    public UpgradeHelper<PlantationLPSUpgrade> SpeedUpgradeManager { get { return _LPSUpgrade; } }
    public UpgradeHelper<PlantationLeavesUpgrade> LeavesUpgradeManager { get { return _leavesUpgrade; } }


    protected override void ConfigureUpgrades()
    {
        ConfigureUpgrade(_stats[0], _regenUpgrade, (upgrade) => { if (RegenUpgraded != null) RegenUpgraded(upgrade); });
        ConfigureUpgrade(_stats[1], _healthUpgrade, (upgrade) => { if (HealthUpgraded != null) HealthUpgraded(upgrade); });
        ConfigureUpgrade(_stats[2], _LPSUpgrade, (upgrade) => { if (LPSUpgraded != null) LPSUpgraded(upgrade); });
        ConfigureUpgrade(_stats[3], _leavesUpgrade, (upgrade) => { if (LeavesUpgraded != null) LeavesUpgraded(upgrade); });
    }
}