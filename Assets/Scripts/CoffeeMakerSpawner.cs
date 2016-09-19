using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoffeeMakerSpawner : MonoBehaviour {

    private List<Vector3> _spawnPoints = new List<Vector3>();

    public GameObject Cafetiere;

    private float _timeBetweenSpawns = 5f;
    const float SPAWN_RATE_MULTIPLIER = 1.5f;
    const int   SPAWN_RATE_INCREASE = 10;

    private float? lastSpawnTime = null;
    const float SPAWN_DISTANCE_X = 4f;
    const float SPAWN_DISTANCE_Y = 2.5f;

    private int _spawnCount;

    // Use this for initialization
    void Start()
    {
        _spawnCount = 0;
        _spawnPoints.Add(new Vector3(-SPAWN_DISTANCE_X, -SPAWN_DISTANCE_Y));
        _spawnPoints.Add(new Vector3(SPAWN_DISTANCE_X, -SPAWN_DISTANCE_Y));
        _spawnPoints.Add(new Vector3(SPAWN_DISTANCE_X, SPAWN_DISTANCE_Y));
        _spawnPoints.Add(new Vector3(-SPAWN_DISTANCE_X, SPAWN_DISTANCE_Y));
    }



	
	// Update is called once per frame
	void Update () {
        if (lastSpawnTime == null || Time.time - lastSpawnTime > _timeBetweenSpawns)
        {
            lastSpawnTime = Time.time;
            SpawnCoffeeMaker();
        }
	}

    private void SpawnCoffeeMaker()
    {
        _spawnCount++;
        if (_spawnCount % SPAWN_RATE_INCREASE == 0)
        {
            _timeBetweenSpawns /= SPAWN_RATE_MULTIPLIER;
            Debug.Log("Increasing spawn rate to " + _timeBetweenSpawns + " after " + _spawnCount + " spawns");
        }
        var randomIndex = new System.Random().Next(_spawnPoints.Count);
        var coffeeMaker = Instantiate(Cafetiere, _spawnPoints[randomIndex], Quaternion.identity) as GameObject;
        coffeeMaker.transform.SetParent(transform);
    }
}
