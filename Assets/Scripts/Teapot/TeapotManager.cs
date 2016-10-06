using UnityEngine;
using System.Collections;

public class TeapotManager : MonoBehaviour {

    public GameObject _teapotPrefab;
    public Steam _steamPrefab;
    
    private float _speed = 1.5f;
    private float _lastAttackTime;
    private int _ammoRemaining;




    private Vector3 _spawnLocation = new Vector3(0, 2, 0);
    private Teapot _teapot;

    private GameController _gameController;
    private HotbarManager _hotbarManager;
    private SteamUpgradeManager _steamUpgradeManager;
   

    void Awake()
    {
        _steamUpgradeManager = FindObjectOfType<SteamUpgradeManager>();
        _gameController = FindObjectOfType<GameController>();
        _hotbarManager = FindObjectOfType<HotbarManager>();
        _teapot = (Instantiate(_teapotPrefab, _spawnLocation, Quaternion.identity)as GameObject).GetComponent<Teapot>();
        _teapot.transform.SetParent(gameObject.transform);

        _lastAttackTime = Time.deltaTime - _steamUpgradeManager.ReloadTimeUpgradeManager.Current.Value;
        _ammoRemaining = (int)_steamUpgradeManager.ClipSizeUpgradeManager.Current.Value;
    }

    void Update ()
    {
        var go = _teapot.gameObject;
        FollowMouse(ref go);
        Move();
        FireAttackSteam();
    }

    private void FireAttackSteam()
    {
        if (Input.GetMouseButtonDown(0) && _ammoRemaining > 0)
        {
            _ammoRemaining--;
            if (_ammoRemaining <= 0)
            {
                StartCoroutine(ReloadAmmo());
            }

            Steam steam = GameObject.Instantiate(_steamPrefab) as Steam;
            steam.transform.position = _teapot.Nose.transform.position;
            var go = steam.gameObject;
            FollowMouse(ref go);
            go.transform.Rotate(new Vector3(0, 180, 0));
            steam.HitEnemy += (enemy) => AttackEnemy(enemy, _steamUpgradeManager.DamageUpgradeManager.Current.Value);
        }
    }

    private IEnumerator ReloadAmmo()
    {
        yield return new WaitForSeconds(_steamUpgradeManager.ReloadTimeUpgradeManager.Current.Value);
        _ammoRemaining = (int)_steamUpgradeManager.ClipSizeUpgradeManager.Current.Value;
    }

    private void AttackEnemy(CoffeeMaker coffeemaker, float attackDamage)
    {
        coffeemaker.TakeDamage(attackDamage);
    }

    private void Move()
    {
        float deltaX = 0, deltaY = 0;
        if (Input.GetKey(KeyCode.W))
            deltaY += _speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            deltaY -= _speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            deltaX -= _speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            deltaX += _speed * Time.deltaTime;
        _teapot.transform.position += new Vector3(deltaX, deltaY, 0);
        EnsureWithinGrid();

    }

    void EnsureWithinGrid()
    {
        var position = _teapot.transform.position;
        _teapot.transform.position = CoffeeMakerSpawner.ForceWithinGrid(position);
    }

    private void FollowMouse(ref GameObject go)
    {
        var cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cameraPosition.z = 0;
        TransformHelper.LookAtTarget(cameraPosition, ref go);
    }
}
