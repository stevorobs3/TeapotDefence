using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoffeeMakerSpawner : MonoBehaviour {

    private List<Vector3> _spawnPoints = new List<Vector3>();

    public GameObject _cafetiere;
    public GameObject _italianStove;

    private float _timeBetweenSpawns;
    private float _minTimeBetweenSpawns = 5f;
    private float _maxTimeBetweenSpawns = 7f;

    const float SPAWN_RATE_MULTIPLIER = 1.25f;

    const int SPAWN_RATE_INCREASE = 10;
    const float MAX_SPAWN_RATE = 0.2f;

    private float? _lastSpawnTime = null;
    const float SPAWN_DISTANCE_X = 5.5f;
    const float SPAWN_DISTANCE_Y = 4.5f;

    const int MIN_SPAWNS = 1;
    const int MAX_SPAWNS = 2;

    private int _spawnCount;

    System.Random _randomGenerator;

    // Use this for initialization
    void Start()
    {
        _randomGenerator = new System.Random();
        NextTimeBetweenSpawns();
        _lastSpawnTime = Time.time;
    }



	
	// Update is called once per frame
	void Update () {
        if (_lastSpawnTime == null || Time.time - _lastSpawnTime > _timeBetweenSpawns)
        {
            NextTimeBetweenSpawns();
            _lastSpawnTime = Time.time;

            int numSpawns = _randomGenerator.Next(MIN_SPAWNS, MAX_SPAWNS + 1);
            SpawnCoffeeMakers(numSpawns);
        }
	}

    private void SpawnCoffeeMakers(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject coffeeMaker = ChooseCoffeeMaker();
            SpawnCoffeeMaker(coffeeMaker);
        }
    }

    private GameObject ChooseCoffeeMaker()
    {
        bool shouldSpawnItalianStove = _randomGenerator.NextBoolean(0.25f);
        if (_spawnCount < 10 || !shouldSpawnItalianStove)
        {
            return _cafetiere;
        }
        else
        {
            return _italianStove;
        }
    }


    private void SpawnCoffeeMaker(GameObject coffeeMakerPrefab)
    {
        _spawnCount++;
        if (_spawnCount % SPAWN_RATE_INCREASE == 0)
        {
            _minTimeBetweenSpawns /= SPAWN_RATE_MULTIPLIER;
            _maxTimeBetweenSpawns /= SPAWN_RATE_MULTIPLIER;
            _minTimeBetweenSpawns = Mathf.Max(MAX_SPAWN_RATE, _minTimeBetweenSpawns);
            _maxTimeBetweenSpawns = Mathf.Max(MAX_SPAWN_RATE, _maxTimeBetweenSpawns);
        }
        Vector3 spawnPoint = RandomSpawnPoint();
        var coffeeMaker = Instantiate(coffeeMakerPrefab, spawnPoint, Quaternion.identity) as GameObject;
        coffeeMaker.transform.SetParent(transform);
    }

    private void NextTimeBetweenSpawns()
    {
        _timeBetweenSpawns = _randomGenerator.NextFloat(_minTimeBetweenSpawns, _maxTimeBetweenSpawns);
    }


    private Vector3 RandomSpawnPoint()
    {
        if (_randomGenerator.NextBoolean())
        {
            bool isRight = _randomGenerator.NextBoolean();
            float x = SPAWN_DISTANCE_X * (isRight ? -1 : 1);
            
            float y = _randomGenerator.NextFloat(-SPAWN_DISTANCE_Y, SPAWN_DISTANCE_Y);
            Debug.Log("Spawning on " + (isRight ? "left" : "right") + " at y " + y);
            return new Vector3(x, y);
        }
        else
        {
            bool isTop = _randomGenerator.NextBoolean();
            float y = SPAWN_DISTANCE_Y * (isTop ? 1 : -1);

            float x = _randomGenerator.NextFloat(-SPAWN_DISTANCE_X, SPAWN_DISTANCE_X);
            Debug.Log("Spawning on " + (isTop ? "top" : "bottom") + " at x " + x);
            return new Vector3(x, y);
        }
    }
}
