using UnityEngine;
using System.Collections.Generic;

public class Plantation : MonoBehaviour {


    public GameObject TeaLeaf;

    public TextMesh LevelText;
    public GameObject LevelPlus;
    public TextMesh UpgradeCostText;
    public GameObject UpgradeInfo;
    

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

    private bool _isUpgradeEnabled; // true if upgrade menu is enabled for this plantation



    private float _lastTeaLeafSpawn;

    private Animator _animator;

    private PlantationUpgradeManager _upgradeManager;

    void Awake()
    {
        UpgradeInfo.SetActive(false);
        _animator = GetComponent<Animator>();
        _health = MaxHealth;
        _gameController = FindObjectOfType<GameController>();
        _plantationManager = FindObjectOfType<PlantationManager>();
        _currencyManager = FindObjectOfType<CurrencyManager>();

        _upgradeManager = new PlantationUpgradeManager();
        
        AssignHealthBar();
        AddSpawnPoints();
        SpawnTeaLeaf();
        AssignUpgrades();
    }

    void AssignUpgrades()
    {
        TeaLeafValue = (int)_upgradeManager.LeavesUpgradeManager.Current.Value;
        TeaLeafColour = _upgradeManager.LeavesUpgradeManager.Current.Colour;
        MaxHealth = _upgradeManager.HealthUpgradeManager.Current.Value;
        LPS = _upgradeManager.LPSUpgradeManager.Current.Value;
        Regen = _upgradeManager.RegenUpgradeManager.Current.Value;
        LevelText.text = _upgradeManager.RegenUpgradeManager.Current.Level.ToString();
        bool canUpgrade = _upgradeManager.RegenUpgradeManager.CanUpgrade();
        LevelPlus.SetActive(canUpgrade);
        if (canUpgrade)
            UpgradeCostText.text = "£" + _upgradeManager.Cost.ToString();
    }

    void OnEnabled()
    {
        _lastTeaLeafSpawn = Time.time;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time - _lastTeaLeafSpawn > 1 / LPS)
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

        if (_isUpgradeEnabled && _upgradeManager.CanUpgrade() && Input.GetMouseButtonDown(0))
            Upgrade();
	}

    void OnMouseEnter()
    {
        if (enabled && _upgradeManager.CanUpgrade())
            ActivateUpdateInfo(true);
    }

    void OnMouseExit()
    {
        if (enabled && _upgradeManager.CanUpgrade())
            ActivateUpdateInfo(false);
    }

    private void Upgrade()
    {
        if (_currencyManager.Spend(_upgradeManager.Cost))
        {
            _upgradeManager.Upgrade();
            AssignUpgrades();
            if (!_upgradeManager.CanUpgrade())
                ActivateUpdateInfo(false);
        }            
    }

    private void ActivateUpdateInfo(bool on)
    {
        Debug.Log("setting active " + on);
        _isUpgradeEnabled = on;
        UpgradeInfo.SetActive(on);
    }

    public bool TakeDamage(float amount)
    {
        _health -= amount;
        ScaleHealthBar();
        bool died = _health <= 0;
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
        _plantationManager.RemovePlantation(this);
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
            _lastTeaLeafSpawn = Time.time;
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
