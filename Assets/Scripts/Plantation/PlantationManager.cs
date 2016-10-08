using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlantationManager : MonoBehaviour {

    public GameObject _teaPlantationPrefab;


    private Dictionary<Vector3, Plantation> _teaPlantations = new Dictionary<Vector3, Plantation>();
    private CurrencyManager _currencyManager;
    private GameController _gameController;
    private HotbarManager _hotbarManager;
    //private PlantationUpgradeManager _upgradeManager;

    const int TEA_PLANTATION_COST = 100;

    Vector3 _spawnLocation = new Vector3(2, 1, 0);

    Plantation _selectedPlantation;

    GameObject _plantationParent;

    // Use this for initialization
    void Awake()
    {
        _plantationParent = new GameObject("PlantationParent");
        //_upgradeManager = FindObjectOfType<PlantationUpgradeManager>();
        _gameController = FindObjectOfType<GameController>();
        _hotbarManager = FindObjectOfType<HotbarManager>();
        SpawnPlantation(_spawnLocation);
        _toPlace = null;
        _currencyManager = FindObjectOfType<CurrencyManager>();


        /*_upgradeManager.HealthUpgraded += UpgradeHealth;
        _upgradeManager.LPSUpgraded += UpgradeLPS;
        _upgradeManager.RegenUpgraded += UpgradeRegen;
        _upgradeManager.LeavesUpgraded += (upgrade) => {
            _selectedPlantation.TeaLeafValue = (int)upgrade.Value;
            _selectedPlantation.TeaLeafColour = upgrade.Colour;
        };*/

        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (_currencyManager.Spend(TEA_PLANTATION_COST))
            {
                
                SpawnPlantation(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        });
    }

    private Plantation _toPlace = null;

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        if (_toPlace != null)
        {
            _toPlace.transform.position = mousePosition;
            if (Input.GetMouseButtonDown(0))
            {
                if (!CoffeeMakerSpawner.WithinGrid(mousePosition)) return;



                mousePosition.z = 0;
                mousePosition.y = Mathf.Round(mousePosition.y);
                mousePosition.x = Mathf.Round(mousePosition.x);
                _toPlace.transform.position = mousePosition;
                _toPlace = null;

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
        //_upgradeManager.SelectPlantation(plantation);   
    }

    private void SpawnPlantation(Vector3 position)
    {
        _gameController.PlantationsBuilt(1);
        var plantation = (Instantiate(_teaPlantationPrefab, position, Quaternion.identity) as GameObject).GetComponent<Plantation>();
        _toPlace = plantation;
        plantation.transform.SetParent(_plantationParent.transform);
        //_upgradeManager.AddPlantation(plantation);
        SelectPlantation(plantation);
        _teaPlantations.Add(position, plantation);
    }
}
