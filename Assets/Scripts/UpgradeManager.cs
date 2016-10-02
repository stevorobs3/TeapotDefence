using System;
using UnityEngine;

public abstract class UpgradeManager : MonoBehaviour
{
    protected UpgradeElement[] _stats;

    CurrencyManager _currencyManager;

    protected void Start()
    {
        if (_currencyManager == null)
        {
            _currencyManager = FindObjectOfType<CurrencyManager>();

            _stats = GetComponentsInChildren<UpgradeElement>();
            ConfigureUpgrades();
        }
    }

    protected abstract void ConfigureUpgrades();
    
    protected void ConfigureUpgrade<T>(UpgradeElement element, UpgradeHelper<T> upgradeManager, Action<T> successAction) where T : Upgrade
    {
        element.Button.onClick.RemoveAllListeners();

        bool hasNextUpgrade = upgradeManager.Next != null;

        element.Button.enabled = hasNextUpgrade;
        element.Cost.enabled = hasNextUpgrade;
        element.BuySection.SetActive(hasNextUpgrade);
        element.Bonus.gameObject.SetActive(hasNextUpgrade);

        if (hasNextUpgrade)
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
            element.Value.text = upgradeManager.Current.Value.ToString();
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
