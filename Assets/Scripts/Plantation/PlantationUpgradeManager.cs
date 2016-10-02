
using System.Collections.Generic;

public class PlantationUpgradeManager : UpgradeManager
{
    public event UpgradeNotificationHandler<PlantationRegenUpgrade> RegenUpgraded;
    public event UpgradeNotificationHandler<PlantationHealthUpgrade> HealthUpgraded;
    public event UpgradeNotificationHandler<PlantationLPSUpgrade> LPSUpgraded;
    public event UpgradeNotificationHandler<PlantationLeavesUpgrade> LeavesUpgraded;

    private class Upgrades
    {
        public Upgrades()
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
    }

    UpgradeMenus _upgradeMenus;
    void Awake()
    {
        _upgradeMenus = FindObjectOfType<UpgradeMenus>();
    }

    Dictionary<Plantation, Upgrades> _plantationUpgrades = new Dictionary<Plantation, Upgrades>();
    Upgrades _selectedUpgrades;

    public void AddPlantation(Plantation plantation)
    {
        _plantationUpgrades.Add(plantation, new Upgrades());
    }

    public void SelectPlantation(Plantation plantation)
    {
        _selectedUpgrades = _plantationUpgrades[plantation];
        if (_stats == null)
            Start();
        else
            ConfigureUpgrades();
        _upgradeMenus.SelectMenuOption(1);
    }

    protected override void ConfigureUpgrades()
    {
        ConfigureUpgrade(_stats[0], _selectedUpgrades.RegenUpgradeManager, (upgrade) => { if (RegenUpgraded != null) RegenUpgraded(upgrade); });
        ConfigureUpgrade(_stats[1], _selectedUpgrades.HealthUpgradeManager, (upgrade) => { if (HealthUpgraded != null) HealthUpgraded(upgrade); });
        ConfigureUpgrade(_stats[2], _selectedUpgrades.LPSUpgradeManager, (upgrade) => { if (LPSUpgraded != null) LPSUpgraded(upgrade); });
        ConfigureUpgrade(_stats[3], _selectedUpgrades.LeavesUpgradeManager, (upgrade) => { if (LeavesUpgraded != null) LeavesUpgraded(upgrade); });
    }
}