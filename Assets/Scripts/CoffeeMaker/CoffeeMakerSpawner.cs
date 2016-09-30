using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMakerSpawner : MonoBehaviour {

    public GameObject _cafetiere;
    public GameObject _italianStove;

    private Dictionary<CoffeeMakerType, GameObject> _coffeeMakerPrefabs;

    private SpawnWave[] _waves;
    private int _nextWaveIndex;

    const float SPAWN_DISTANCE_X = 4.5f;
    const float SPAWN_DISTANCE_Y = 3.5f;

    System.Random _randomGenerator;

    private SpawnInformation _spawnInformation;
    private GameController _gameController;

    // Use this for initialization
    IEnumerator Start()
    {
        _coffeeMakerPrefabs = new Dictionary<CoffeeMakerType, GameObject>()
        {
            {CoffeeMakerType.Cafetiere, _cafetiere },
            {CoffeeMakerType.ItalianStove, _italianStove },
        };

        _randomGenerator = new System.Random();

        _spawnInformation = FindObjectOfType<SpawnInformation>();
        _gameController = FindObjectOfType<GameController>();

        _waves = SpawnWave.Levels;
        _nextWaveIndex = -1;

        yield return SpawnNextWave();
    }

    private void SpawnCoffeeMaker(CoffeeMakerType coffeeMakerType)
    {
        GameObject coffeeMakerPrefab = _coffeeMakerPrefabs[coffeeMakerType];
        Vector3 spawnPoint = RandomSpawnPoint();
        var coffeeMaker = Instantiate(coffeeMakerPrefab, spawnPoint, Quaternion.identity) as GameObject;
        coffeeMaker.transform.SetParent(transform);
        _spawnInformation.EntitySpawned();
    }

    private IEnumerator SpawnNextWave()
    {
        _nextWaveIndex++;
        if (_nextWaveIndex >= _waves.Length - 1)
        {
            _gameController.EndGame(win: true);
            yield return null;
        }
        else
        {
            SpawnWave nextWave = _waves[_nextWaveIndex];
            yield return SpawnNextWave(nextWave);
        }
    }


    private IEnumerator SpawnNextWave(SpawnWave wave)
    {
        _spawnInformation.SetNextSpawn(wave);

        yield return new WaitForSeconds(wave.TimeBeforeFirstSpawn);
        
        yield return SpawnCoffeeMakers(CoffeeMakerType.Cafetiere, wave.NumCafetieres, wave.TimeBetweenSpawns);
        yield return SpawnCoffeeMakers(CoffeeMakerType.ItalianStove, wave.NumItalianStoves, wave.TimeBetweenSpawns);
        yield return SpawnNextWave();
    }


    private IEnumerator SpawnCoffeeMakers(CoffeeMakerType type, int amount, float timeBetweenSpawns)
    {
        for (int i = 0; i < amount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns);
            SpawnCoffeeMaker(type);
        }
    }

    private Vector3 RandomSpawnPoint()
    {
        if (_randomGenerator.NextBoolean())
        {
            bool isRight = _randomGenerator.NextBoolean();
            float x = SPAWN_DISTANCE_X * (isRight ? -1 : 1);
            
            float y = _randomGenerator.NextFloat(-SPAWN_DISTANCE_Y, SPAWN_DISTANCE_Y);
            return new Vector3(x, y);
        }
        else
        {
            bool isTop = _randomGenerator.NextBoolean();
            float y = SPAWN_DISTANCE_Y * (isTop ? 1 : -1);

            float x = _randomGenerator.NextFloat(-SPAWN_DISTANCE_X, SPAWN_DISTANCE_X);
            return new Vector3(x, y);
        }
    }
}
