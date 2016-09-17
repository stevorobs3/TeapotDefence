using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeaPlantationManager : MonoBehaviour {

    public GameObject _teaPlantationPrefab;


    private Dictionary<Vector3, TeaPlantation> _teaPlantations = new Dictionary<Vector3, TeaPlantation>();
    private CurrencyManager _currencyManager;

    const int TEA_PLANTATION_COST = 10;

	// Use this for initialization
	void Start () {
        SpawnTeaPlantation(Vector3.zero);
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
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
            else if (_currencyManager.Spend(TEA_PLANTATION_COST)) {
                SpawnTeaPlantation(spawnLocation);
            }
        }
	}
    private void SpawnTeaPlantation(Vector3 position)
    {
        var teaPlantation = (Instantiate(_teaPlantationPrefab, position, Quaternion.identity) as GameObject).GetComponent<TeaPlantation>();
        _teaPlantations.Add(position, teaPlantation);
    }
}
