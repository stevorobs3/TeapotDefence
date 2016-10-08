using System;
using UnityEngine;

public abstract class UpgradeManager : MonoBehaviour
{
    protected UpgradeElement[] _stats;

    public delegate void InitializedHandler();
    public event InitializedHandler Initialized;

    CurrencyManager _currencyManager;

    protected void Start()
    {
        if (_currencyManager == null)
        {
            _currencyManager = FindObjectOfType<CurrencyManager>();

            _stats = GetComponentsInChildren<UpgradeElement>();
            ConfigureUpgrades();
            if (Initialized != null)
                Initialized();
        }
    }

    protected abstract void ConfigureUpgrades();
    
    protected void ConfigureUpgrade<T>(UpgradeElement element, UpgradeHelper<T> upgradeManager, Action<T> successAction) where T : Upgrade
    {
        element.CostButton.onClick.RemoveAllListeners();

        bool hasNextUpgrade = upgradeManager.Next != null;

        element.CostButton.enabled = hasNextUpgrade;

        for (int i = 0; i < element.Levels.Length; i++)
        {
            element.Levels[i].color = (i < upgradeManager.Current.Level) ? new Color(186f / 255f, 0, 0, 1f) : Color.white;
        }

        if (hasNextUpgrade)
        {
            element.CostButton.onClick.AddListener(() => UpgradeStat(upgradeManager, successAction));
            element.Name.text = upgradeManager.Next.Title;
            element.CostText.text = upgradeManager.Next.Cost.ToString();
        }        
    }

    protected void UpgradeStat<T>(UpgradeHelper<T> upgradeManager, Action<T> onSuccess) where T : Upgrade
    {
        if (upgradeManager.CanUpgrade() && _currencyManager.Spend(upgradeManager.Next.Cost))
        {
            upgradeManager.Upgrade();
            onSuccess(upgradeManager.Current);
            ConfigureUpgrades();
        }
    }
}
