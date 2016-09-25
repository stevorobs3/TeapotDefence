using System;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    private UpgradeElement[] _stats;

    public delegate void UpgradeNotificationHandler<T>(T upgrade);
    public event UpgradeNotificationHandler<TeapotRangeUpgrade> RangeUpgraded;
    public event UpgradeNotificationHandler<TeapotDPSUpgrade>   DPSUpgraded;
    public event UpgradeNotificationHandler<TeapotSpeedUpgrade> SpeedUpgraded;


    CurrencyManager _currencyManager;

    void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();

        _dpsUpgrade = new TeapotUpgradeManager<TeapotDPSUpgrade>(TeapotDPSUpgrade.Upgrades);
        _rangeUpgrade = new TeapotUpgradeManager<TeapotRangeUpgrade>(TeapotRangeUpgrade.Upgrades);
        _speedUpgrade = new TeapotUpgradeManager<TeapotSpeedUpgrade>(TeapotSpeedUpgrade.Upgrades);

        _stats = GetComponentsInChildren<UpgradeElement>();
        if (_stats.Length != 3)
            Debug.LogError("Not enough upgrade elements!");
        ConfigureUpgrades();
    }

    private TeapotUpgradeManager<TeapotDPSUpgrade> _dpsUpgrade;
    private TeapotUpgradeManager<TeapotRangeUpgrade> _rangeUpgrade;
    private TeapotUpgradeManager<TeapotSpeedUpgrade> _speedUpgrade;

    public TeapotUpgradeManager<TeapotDPSUpgrade> DPSUpgradeManager {  get { return _dpsUpgrade; } }
    public TeapotUpgradeManager<TeapotRangeUpgrade> RangeUpgradeManager { get { return _rangeUpgrade; } }
    public TeapotUpgradeManager<TeapotSpeedUpgrade> SpeedUpgradeManager { get { return _speedUpgrade; } }



    void ConfigureUpgrades()
    {
        ConfigureUpgrade(_stats[0], _dpsUpgrade, (upgrade) => { if (DPSUpgraded != null) DPSUpgraded(upgrade); });
        ConfigureUpgrade(_stats[1], _rangeUpgrade, (upgrade) => { if (RangeUpgraded != null) RangeUpgraded(upgrade); });
        ConfigureUpgrade(_stats[2], _speedUpgrade, (upgrade) => { if (SpeedUpgraded != null) SpeedUpgraded(upgrade); });
    }


    void ConfigureUpgrade<T>(UpgradeElement element, TeapotUpgradeManager<T> upgradeManager, Action<T> successAction) where T : TeapotUpgrade
    {
        element.Button.onClick.RemoveAllListeners();
        if (upgradeManager.Next != null)
        {
            element.Button.onClick.AddListener(() => UpgradeStat(upgradeManager, successAction));
            element.Title.text = upgradeManager.Next.Title;

            float bonus = upgradeManager.Next.Value - upgradeManager.Current.Value;
            element.Bonus.text = "(+" + bonus.ToString() + ")";
            element.Value.text = upgradeManager.Current.Value.ToString();
            element.Cost.text = upgradeManager.Next.Cost.ToString();
        }
        else
        {
            element.Button.enabled = false;
            element.Cost.enabled = false;
            element.BuySection.SetActive(false);
            element.Bonus.gameObject.SetActive(false);
        }        
    }

    void UpgradeStat<T>(TeapotUpgradeManager<T> upgradeManager, Action<T> onSuccess) where T : TeapotUpgrade
    {
        if (upgradeManager.CanUpgrade() && _currencyManager.Spend(upgradeManager.Next.Cost))
        {
            upgradeManager.Upgrade();
            onSuccess(upgradeManager.Current);
            ConfigureUpgrades();
        }
    }
}
