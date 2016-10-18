
public class PlantationUpgradeManager
{
    public PlantationUpgradeManager()
    {
        _regenUpgrade = new UpgradeHelper<PlantationRegenUpgrade>(PlantationRegenUpgrade.Upgrades);
        _healthUpgrade = new UpgradeHelper<PlantationHealthUpgrade>(PlantationHealthUpgrade.Upgrades);
        _LPSUpgrade = new UpgradeHelper<PlantationLPSUpgrade>(PlantationLPSUpgrade.Upgrades);
        _leavesUpgrade = new UpgradeHelper<PlantationLeavesUpgrade>(PlantationLeavesUpgrade.Upgrades);
    }

    private UpgradeHelper<PlantationRegenUpgrade> _regenUpgrade;
    private UpgradeHelper<PlantationHealthUpgrade> _healthUpgrade;
    private UpgradeHelper<PlantationLPSUpgrade> _LPSUpgrade;
    private UpgradeHelper<PlantationLeavesUpgrade> _leavesUpgrade;

    public UpgradeHelper<PlantationRegenUpgrade> RegenUpgradeManager { get { return _regenUpgrade; } }
    public UpgradeHelper<PlantationHealthUpgrade> HealthUpgradeManager { get { return _healthUpgrade; } }
    public UpgradeHelper<PlantationLPSUpgrade> LPSUpgradeManager { get { return _LPSUpgrade; } }
    public UpgradeHelper<PlantationLeavesUpgrade> LeavesUpgradeManager { get { return _leavesUpgrade; } }

    public bool Upgrade()
    {
        return _regenUpgrade.Upgrade() && _healthUpgrade.Upgrade() && _LPSUpgrade.Upgrade() && _leavesUpgrade.Upgrade();
    }

    public bool CanUpgrade()
    {
        return _regenUpgrade.CanUpgrade();
    }

    public int Cost
    {
        get
        {
            return _regenUpgrade.Next.Cost + _healthUpgrade.Next.Cost + _LPSUpgrade.Next.Cost + _leavesUpgrade.Next.Cost;
        }
    }
}