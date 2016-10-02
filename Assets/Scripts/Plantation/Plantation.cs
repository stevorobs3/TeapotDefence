using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Plantation : MonoBehaviour {


    public GameObject TeaLeaf;


    public void Select()
    {
        _animator.SetBool("Selected", true);

    }
    
    public void UnSelect()
    {
        _animator.SetBool("Selected", false);
    }

    static int Bonus = 1;

    public int TeaLeafValue = (int)PlantationLeavesUpgrade.Level0.Value;
    public Color TeaLeafColour = PlantationLeavesUpgrade.Level0.Colour;
    public float MaxHealth = PlantationHealthUpgrade.Level0.Value;
    public float LPS = PlantationLPSUpgrade.Level0.Value;
    public float Regen = PlantationRegenUpgrade.Level0.Value;

    float _health;

    private List<Transform> _spawnPoints = new List<Transform>();
    private Dictionary<Vector3, GameObject> _teaLeaves = new Dictionary<Vector3, GameObject>();

    private CurrencyManager _currencyManager;
    private PlantationManager _plantationManager;
    private GameController _gameController;

    private GameObject _healthBar;


    private float lastTeaLeafSpawn;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start () {
        _health = MaxHealth;
        _gameController = FindObjectOfType<GameController>();
        _plantationManager = FindObjectOfType<PlantationManager>();

        AssignHealthBar();
        AddSpawnPoints();
        SpawnTeaLeaf();
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }

    // Update is called once per frame
    void Update () {
        if (Time.time - lastTeaLeafSpawn > 1 / LPS)
        {
            SpawnTeaLeaf();
        }
        _health += Time.deltaTime * Regen;
        _health = Mathf.Min(_health, MaxHealth);
        ScaleHealthBar();

        foreach (var leaf in _teaLeaves.Values)
        {
            leaf.GetComponent<SpriteRenderer>().color = TeaLeafColour;
        }
	}

    public bool TakeDamage(float amount)
    {
        _health -= amount;
        ScaleHealthBar();
        bool died = _health <= -0;
        if (died)
            Die();
        return died; 
    }

    void ScaleHealthBar()
    {
        var localScale = _healthBar.transform.localScale;
        localScale.x = (float)_health / MaxHealth;
        _healthBar.transform.localScale = localScale;
    }

    private void Die()
    {
        _plantationManager.RemoveTeaPlantation(transform.position);
        Destroy(gameObject);
    }

    public void Harvest()
    {
        var bonus = _teaLeaves.Count == _spawnPoints.Count ? Bonus : 0;
        var teaLeavesHarvested = _teaLeaves.Count * TeaLeafValue + bonus;
        _gameController.TeaLeavesHarvested(teaLeavesHarvested);
        _currencyManager.Deposit(teaLeavesHarvested);
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
        else {
            Harvest();
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
            if (child.name.Contains("SpawnPoint"))
                _spawnPoints.Add(child);
        }
    }

    void AssignHealthBar()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("HealthBar"))
                _healthBar = child.gameObject;
        }
    }
}
