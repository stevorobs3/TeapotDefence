using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlantationManager : MonoBehaviour {

    public GameObject _teaPlantationPrefab;


    private Dictionary<Vector3, Plantation> _teaPlantations = new Dictionary<Vector3, Plantation>();
    private CurrencyManager _currencyManager;
    private GameController _gameController;
    private HotbarManager _hotbarManager;
    private PlantationUpgradeManager _upgradeManager;


    const int TEA_PLANTATION_COST = 10;

	// Use this for initialization
	void Awake () {
        _upgradeManager = FindObjectOfType<PlantationUpgradeManager>();
        _gameController = FindObjectOfType<GameController>();
        _hotbarManager = FindObjectOfType<HotbarManager>();
        SpawnTeaPlantation(Vector3.zero);
        _currencyManager = FindObjectOfType<CurrencyManager>();


        _upgradeManager.HealthUpgraded += UpgradeHealth;
        _upgradeManager.LPSUpgraded += UpgradeLPS;
        _upgradeManager.RegenUpgraded += UpgradeRegen;
        _upgradeManager.LeavesUpgraded += (upgrade) => {
            Plantation.TeaLeafValue = (int)upgrade.Value;
            Plantation.TeaLeafColour = upgrade.Colour;
        };

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnLocation.z = 0;
            spawnLocation.y = Mathf.Round(spawnLocation.y);
            spawnLocation.x = Mathf.Round(spawnLocation.x);
            
            if (_teaPlantations.ContainsKey(spawnLocation))
            {
                Debug.Log("Harvesting!");
                _teaPlantations[spawnLocation].Harvest();
            }
            else if (_hotbarManager.CurrentlySelectedItem() == SelectedItem.TeaPlantation && _currencyManager.Spend(TEA_PLANTATION_COST)) {
                SpawnTeaPlantation(spawnLocation);
            }
        }
	}

    public void RemoveTeaPlantation(Vector3 position)
    {
        _teaPlantations.Remove(position);
    }

    void UpgradeHealth(PlantationHealthUpgrade upgrade)
    {
        Plantation.MaxHealth = upgrade.Value;
    }

    void UpgradeRegen(PlantationRegenUpgrade upgrade)
    {
        Plantation.Regen = upgrade.Value;
    }

    void UpgradeLPS(PlantationLPSUpgrade upgrade)
    {
        Plantation.LPS = upgrade.Value;
    }

    private void SpawnTeaPlantation(Vector3 position)
    {
        _gameController.PlantationsBuilt(1);
        var teaPlantation = (Instantiate(_teaPlantationPrefab, position, Quaternion.identity) as GameObject).GetComponent<Plantation>();
        teaPlantation.transform.SetParent(transform);
        _teaPlantations.Add(position, teaPlantation);
    }
}
