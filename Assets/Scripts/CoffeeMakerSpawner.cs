using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoffeeMakerSpawner : MonoBehaviour {

    private List<Vector3> _spawnPoints = new List<Vector3>();

    public GameObject Cafetiere;

    private float TIME_BETWEEN_SPAWNS = 5f;
    private float? lastSpawnTime = null;

    
    // Use this for initialization
    void Start () {
        _spawnPoints.Add(new Vector3(-10,-10));
        _spawnPoints.Add(new Vector3(10, -10));
        _spawnPoints.Add(new Vector3(10, 10));
        _spawnPoints.Add(new Vector3(-10, 10));
        
    }


	
	// Update is called once per frame
	void Update () {
        if (lastSpawnTime == null || Time.time - lastSpawnTime > TIME_BETWEEN_SPAWNS)
        {
            lastSpawnTime = Time.time;
            SpawnCoffeeMaker();
        }
	}

    private void SpawnCoffeeMaker()
    {
        var randomIndex = new System.Random().Next(_spawnPoints.Count);
        Instantiate(Cafetiere, _spawnPoints[randomIndex], Quaternion.identity);
    }
}
