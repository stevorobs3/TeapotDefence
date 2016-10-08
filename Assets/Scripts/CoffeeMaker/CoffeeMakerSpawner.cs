using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoffeeMakerSpawner : MonoBehaviour {

    public GameObject _cafetiere;
    public GameObject _italianStove;

    private Dictionary<CoffeeMakerType, GameObject> _coffeeMakerPrefabs;

    private SpawnWave[] _waves;

    private SpawnWave _currentWave;
    private int _coffeeMakersAlive = 0;
    private int _nextWaveIndex;



    const float SPAWN_DISTANCE_X_MIN = -4.5f;
    const float SPAWN_DISTANCE_X_MAX = 4.5f;
    const float SPAWN_DISTANCE_Y_MIN = -3.5f;
    const float SPAWN_DISTANCE_Y_MAX = 6.5f;

    public static bool WithinGrid(Vector3 spawnLocation)
    {
        bool xOK = spawnLocation.x >= SPAWN_DISTANCE_X_MIN && spawnLocation.x <= SPAWN_DISTANCE_X_MAX;
        bool yOK = spawnLocation.y >= SPAWN_DISTANCE_Y_MIN && spawnLocation.y <= SPAWN_DISTANCE_Y_MAX;
        return xOK && yOK;
    }

    public static Vector3 ForceWithinGrid(Vector3 position)
    {
        position.x = Mathf.Max(Mathf.Min(position.x, SPAWN_DISTANCE_X_MAX), SPAWN_DISTANCE_X_MIN);
        position.y = Mathf.Max(Mathf.Min(position.y, SPAWN_DISTANCE_Y_MAX), SPAWN_DISTANCE_Y_MIN);
        return position;
    }

    System.Random _randomGenerator;

    private GameController _gameController;
    private TeapotManager _teapotManager;

    GameObject _shop;
    // Use this for initialization
    IEnumerator Start()
    {
        _shop = GameObject.Find("Shop");
        _teapotManager = FindObjectOfType<TeapotManager>();


        _coffeeMakerPrefabs = new Dictionary<CoffeeMakerType, GameObject>()
        {
            {CoffeeMakerType.Cafetiere, _cafetiere },
            {CoffeeMakerType.ItalianStove, _italianStove },
        };
        HideShop();

        _randomGenerator = new System.Random();

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
        coffeeMaker.GetComponent<CoffeeMaker>().Died += () => _coffeeMakersAlive--;
    }

    private IEnumerator SpawnNextWave()
    {
        _nextWaveIndex++;
        if (_nextWaveIndex >= _waves.Length - 1)
        {
            while (_coffeeMakersAlive != 0)
                yield return null;
            _gameController.EndGame(win: true);
        }
        else
        {
            if (_nextWaveIndex != 0)
            {
                while (_coffeeMakersAlive != 0)
                    yield return null;
                yield return ShowShop();
            }
            SpawnWave nextWave = _waves[_nextWaveIndex];
            yield return SpawnNextWave(nextWave);
        }
    }

    private IEnumerator ShowShop()
    {
        _teapotManager.ShowShop();
        _shop.GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, Screen.height / 2);
        
        bool shopIsOpen = true;
        _shop.transform.FindChild("NextButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            shopIsOpen = false;
            HideShop();
        });

        while (shopIsOpen)
            yield return null;
    }

    private void HideShop()
    {
        _teapotManager.HideShop();
        _shop.GetComponent<RectTransform>().position = new Vector3(10000, 10000);
    }



    private IEnumerator SpawnNextWave(SpawnWave wave)
    {
        _currentWave = wave;
        _coffeeMakersAlive += wave.Count;
        
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
            float x = isRight ? SPAWN_DISTANCE_X_MIN : SPAWN_DISTANCE_X_MAX;
            
            float y = _randomGenerator.NextFloat(SPAWN_DISTANCE_Y_MIN, SPAWN_DISTANCE_Y_MAX);
            return new Vector3(x, y);
        }
        else
        {
            bool isTop = _randomGenerator.NextBoolean();
            float y = isTop ? SPAWN_DISTANCE_Y_MAX : SPAWN_DISTANCE_Y_MIN;

            float x = _randomGenerator.NextFloat(SPAWN_DISTANCE_X_MIN, SPAWN_DISTANCE_X_MAX);
            return new Vector3(x, y);
        }
    }
}
