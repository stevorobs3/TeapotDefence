using System;
using UnityEngine;

public abstract class UpgradeManager : MonoBehaviour
{
    protected UpgradeElement[] _stats;

    CurrencyManager _currencyManager;

    protected void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();

        _stats = GetComponentsInChildren<UpgradeElement>();
        ConfigureUpgrades();
    }

    protected abstract void ConfigureUpgrades();
    
    protected void ConfigureUpgrade<T>(UpgradeElement element, UpgradeHelper<T> upgradeManager, Action<T> successAction) where T : Upgrade
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
