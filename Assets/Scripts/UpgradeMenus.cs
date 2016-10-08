using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeMenus : MonoBehaviour {

    SteamUpgradeManager _steamUpgradeManager;
    LidUpgradeManager _lidUpgradeManager;

    void Awake()
    {
        _steamUpgradeManager = GetComponentInChildren<SteamUpgradeManager>();
        _lidUpgradeManager = GetComponentInChildren<LidUpgradeManager>();


        //SelectMenuOption((int)UpgradeMenuType.Steam);
    }

    public enum UpgradeMenuType {Steam, Lid};

    public void SelectMenuOption(int menu)
    {
        SelectOption(menu == 0, _steamUpgradeManager);
        SelectOption(menu == 1, _lidUpgradeManager);
    }

    private static void SelectOption(bool isMenu, MonoBehaviour upgrade)
    {
        upgrade.transform.Find("Pane").gameObject.SetActive(isMenu);
        var images = upgrade.GetComponentsInChildren<Image>();
        foreach (var image in images)
        {
            var color = image.color;
            color.a = isMenu ? 1f : 0.5f;
            image.color = color;

            if (isMenu)
            {
                upgrade.GetComponent<RectTransform>().SetAsLastSibling();
            }
        }
    }
}
