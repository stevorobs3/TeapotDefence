using UnityEngine;
using System.Collections.Generic;

public class PlantationManager : MonoBehaviour {

    public GameObject _teaPlantationPrefab;


    private Dictionary<Vector3, Plantation> _teaPlantations = new Dictionary<Vector3, Plantation>();
    private CurrencyManager _currencyManager;
    private GameController _gameController;
    private HotbarManager _hotbarManager;
    private PlantationUpgradeManager _upgradeManager;

    const int TEA_PLANTATION_COST = 10;

    Vector3 _spawnLocation = new Vector3(0, 1, 0);

    Plantation _selectedPlantation;

    // Use this for initialization
    void Awake () {
        _upgradeManager = FindObjectOfType<PlantationUpgradeManager>();
        _gameController = FindObjectOfType<GameController>();
        _hotbarManager = FindObjectOfType<HotbarManager>();
        SpawnPlantation(_spawnLocation);
        _currencyManager = FindObjectOfType<CurrencyManager>();


        _upgradeManager.HealthUpgraded += UpgradeHealth;
        _upgradeManager.LPSUpgraded += UpgradeLPS;
        _upgradeManager.RegenUpgraded += UpgradeRegen;
        _upgradeManager.LeavesUpgraded += (upgrade) => {
            _selectedPlantation.TeaLeafValue = (int)upgrade.Value;
            _selectedPlantation.TeaLeafColour = upgrade.Colour;
        };

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            var spawnLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (!CoffeeMakerSpawner.WithinGrid(spawnLocation)) return;

            spawnLocation.z = 0;
            spawnLocation.y = Mathf.Round(spawnLocation.y);
            spawnLocation.x = Mathf.Round(spawnLocation.x);
            
            if (_teaPlantations.ContainsKey(spawnLocation))
            {
                SelectPlantation(_teaPlantations[spawnLocation]);
            }
            else if (_hotbarManager.CurrentlySelectedItem() == SelectedItem.TeaPlantation && _currencyManager.Spend(TEA_PLANTATION_COST)) {
                SpawnPlantation(spawnLocation);
            }
        }
	}

    public void RemoveTeaPlantation(Vector3 position)
    {
        _teaPlantations.Remove(position);
    }

    void UpgradeHealth(PlantationHealthUpgrade upgrade)
    {
        _selectedPlantation.MaxHealth = upgrade.Value;
    }

    void UpgradeRegen(PlantationRegenUpgrade upgrade)
    {
        _selectedPlantation.Regen = upgrade.Value;
    }

    void UpgradeLPS(PlantationLPSUpgrade upgrade)
    {
        _selectedPlantation.LPS = upgrade.Value;
    }

    void SelectPlantation(Plantation plantation)
    {
        if (_selectedPlantation != null)
            _selectedPlantation.UnSelect();
        _selectedPlantation = plantation;
        plantation.Select();
        _upgradeManager.SelectPlantation(plantation);   
    }

    private void SpawnPlantation(Vector3 position)
    {
        _gameController.PlantationsBuilt(1);
        var plantation = (Instantiate(_teaPlantationPrefab, position, Quaternion.identity) as GameObject).GetComponent<Plantation>();
        plantation.transform.SetParent(transform);
        _upgradeManager.AddPlantation(plantation);
        SelectPlantation(plantation);
        _teaPlantations.Add(position, plantation);
    }
}
