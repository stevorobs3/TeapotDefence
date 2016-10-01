﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeMenus : MonoBehaviour {

    public GameObject _teapotUpgradeMenu;
    public GameObject _plantationUpgradeMenu;

    private Dictionary<UpgradeMenuType, GameObject> _menus;

    void Start()
    {
        _menus = new Dictionary<UpgradeMenuType, GameObject>()
        {
            {UpgradeMenuType.Teapot, _teapotUpgradeMenu },
            {UpgradeMenuType.Plantation, _plantationUpgradeMenu }
        };
        SelectMenuOption(0);
    }

    public enum UpgradeMenuType { Teapot, Plantation};

    public void SelectMenuOption(int menu)
    {
        foreach (var keyValue in _menus)
        {
            var images = keyValue.Value.GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                var color = image.color;
                //if (image.gameObject.name != "Tab" || (int)keyValue.Key != menu)
                //{
                //    color.a = 0.5f;
                //    image.color = color;
                //}
                //else
                //{
                //    color.a = 1f;
                //    image.color = color;
                //}

                if ((int)keyValue.Key == menu)
                {
                    keyValue.Value.GetComponent<RectTransform>().SetAsLastSibling();
                    keyValue.Value.transform.Find("Pane").gameObject.SetActive(true);
                }
                else
                    keyValue.Value.transform.Find("Pane").gameObject.SetActive(false);
            }            
        }
    }
}
