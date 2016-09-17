using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeaPlantation : MonoBehaviour {


    public GameObject TeaLeaf;

    const float TIME_BETWEEN_SPAWNS = 2F;
    const int TEAF_LEAF_VALUE = 1;

    private List<Transform> _spawnPoints = new List<Transform>();
    private Dictionary<Vector3, GameObject> _teaLeaves = new Dictionary<Vector3, GameObject>();

    private CurrencyManager _currencyManager;


    private float lastTeaLeafSpawn;
    // Use this for initialization
    void Start () {
        AddSpawnPoints();
        SpawnTeaLeaf();
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }

    // Update is called once per frame
    void Update () {
        if (Time.time - lastTeaLeafSpawn > TIME_BETWEEN_SPAWNS)
        {
            SpawnTeaLeaf();
        }        	
	}

    public void Harvest()
    {
        _currencyManager.Deposit(_teaLeaves.Count * TEAF_LEAF_VALUE);
        foreach (var teaLeaf in _teaLeaves)
        {
            Destroy(teaLeaf.Value);
        }
        _teaLeaves.Clear();
    }

    void SpawnTeaLeaf()
    {
        if (_teaLeaves.Keys.Count != _spawnPoints.Count)
        {
            lastTeaLeafSpawn = Time.time;
            var spawnLocation = RandomFreeSpawnPosition();
            var teaLeaf = Instantiate(TeaLeaf, spawnLocation, RandomRotation()) as GameObject;
            teaLeaf.transform.SetParent(transform);
            _teaLeaves.Add(spawnLocation, teaLeaf);
        }
    }

    Vector3 RandomFreeSpawnPosition()
    {
        var random = new System.Random();
        int? freeIndex = null;
        while(freeIndex == null)
        {
            var nextIndex = random.Next(_spawnPoints.Count);
            if (!_teaLeaves.ContainsKey(_spawnPoints[nextIndex].position))
                freeIndex = nextIndex;
        }            

        return _spawnPoints[(int)freeIndex].position;
    }
        
    Quaternion RandomRotation()
    {
        return Quaternion.identity;
    }

    void AddSpawnPoints()
    {
        foreach (Transform child in transform)
        {
            _spawnPoints.Add(child);
        }
    }
}
