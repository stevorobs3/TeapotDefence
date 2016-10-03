using UnityEngine;

public class ExplosivesManager : MonoBehaviour
{
    public Explosive _notNiceBiscuitPrefab;

    private HotbarManager _hotbarManager;
    private CurrencyManager _currencyManager;

    void Awake()
    {
        _hotbarManager = FindObjectOfType<HotbarManager>();
        _currencyManager = FindObjectOfType<CurrencyManager>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _hotbarManager.CurrentlySelectedItem() == SelectedItem.NotNiceBiscuit)
        {
            if (_currencyManager.Spend(_notNiceBiscuitPrefab.Cost))
            {
                var spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                spawnLocation.z = 0;
                var go = GameObject.Instantiate(_notNiceBiscuitPrefab.gameObject, spawnLocation, Quaternion.identity) as GameObject;
                go.transform.SetParent(transform);
            }
        }
    }
}