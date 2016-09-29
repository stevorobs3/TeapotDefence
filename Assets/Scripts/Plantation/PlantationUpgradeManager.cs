
public class PlantationUpgradeManager : UpgradeManager
{
    public event UpgradeNotificationHandler<PlantationRegenUpgrade> RegenUpgraded;
    public event UpgradeNotificationHandler<PlantationHealthUpgrade> HealthUpgraded;
    public event UpgradeNotificationHandler<PlantationLPSUpgrade> LPSUpgraded;
    
    new void Awake()
    {
        _regenUpgrade = new UpgradeHelper<PlantationRegenUpgrade>(PlantationRegenUpgrade.Upgrades);
        _healthUpgrade = new UpgradeHelper<PlantationHealthUpgrade>(PlantationHealthUpgrade.Upgrades);
        _LPSUpgrade = new UpgradeHelper<PlantationLPSUpgrade>(PlantationLPSUpgrade.Upgrades);

        base.Awake();
    }

    private UpgradeHelper<PlantationRegenUpgrade> _regenUpgrade;
    private UpgradeHelper<PlantationHealthUpgrade> _healthUpgrade;
    private UpgradeHelper<PlantationLPSUpgrade> _LPSUpgrade;

    public UpgradeHelper<PlantationRegenUpgrade> DPSUpgradeManager {  get { return _regenUpgrade; } }
    public UpgradeHelper<PlantationHealthUpgrade> RangeUpgradeManager { get { return _healthUpgrade; } }
    public UpgradeHelper<PlantationLPSUpgrade> SpeedUpgradeManager { get { return _LPSUpgrade; } }
    

    protected override void ConfigureUpgrades()
    {
        ConfigureUpgrade(_stats[0], _regenUpgrade, (upgrade) => { if (RegenUpgraded != null) RegenUpgraded(upgrade); });
        ConfigureUpgrade(_stats[1], _healthUpgrade, (upgrade) => { if (HealthUpgraded != null) HealthUpgraded(upgrade); });
        ConfigureUpgrade(_stats[2], _LPSUpgrade, (upgrade) => { if (LPSUpgraded != null) LPSUpgraded(upgrade); });
    }
}