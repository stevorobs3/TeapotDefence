using UnityEngine;
using System.Collections;

public class TeapotManager : MonoBehaviour {

    public GameObject _teapotPrefab;
    public Steam _steamPrefab;
    public SteamUpgradeManager _steamUpgradeManager;

    private float _speed = 1.5f;
    private int _ammoRemaining;
    private bool _reloading;

    private WeaponView _steamWeaponView;

    private Vector3 _spawnLocation = new Vector3(2, 2, 0);
    private Teapot _teapot;

    private bool _shopIsVisible = false;

    void Awake()
    {
        _steamUpgradeManager = FindObjectOfType<SteamUpgradeManager>();
        _steamWeaponView = FindObjectOfType<WeaponView>();
        _teapot = (Instantiate(_teapotPrefab, _spawnLocation, Quaternion.identity)as GameObject).GetComponent<Teapot>();
        _teapot.transform.SetParent(gameObject.transform);


        _steamUpgradeManager.Initialized += () =>
        {
            float currentReloadTime = _steamUpgradeManager.ReloadTime.Current.Value;
            _ammoRemaining = (int)_steamUpgradeManager.ClipSize.Current.Value;

            _steamWeaponView.SetClipSize(_ammoRemaining);

            _steamUpgradeManager.ClipSizeUpgraded += (clipSize) => _steamWeaponView.SetClipSize((int)clipSize.Value);
        };
    }

    void Update ()
    {
        var go = _teapot.gameObject;
        FollowMouse(ref go);
        Move();
        FireAttackSteam();
        LookForReload();
    }

    void LookForReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && _ammoRemaining < (int)_steamUpgradeManager.ClipSize.Current.Value && !_reloading)
        {
            StartCoroutine(ReloadAmmo());
        }
    }

    public void ShowShop()
    {
        _shopIsVisible = true;
    }

    public void HideShop()
    {
        _shopIsVisible = false;
    }

    private void FireAttackSteam()
    {
        if (Input.GetMouseButtonDown(0) && _ammoRemaining > 0 && !_reloading && !_shopIsVisible)
        {
            _ammoRemaining--;
            _steamWeaponView.UseBullet();
            if (_ammoRemaining <= 0)
            {
                StartCoroutine(ReloadAmmo());
            }

            Steam steam = GameObject.Instantiate(_steamPrefab) as Steam;
            steam.transform.position = _teapot.Nose.transform.position;
            var go = steam.gameObject;
            FollowMouse(ref go);
            go.transform.Rotate(new Vector3(0, 180, 0));
            steam.HitEnemy += (enemy) => AttackEnemy(enemy, _steamUpgradeManager.Damage.Current.Value);
        }
    }

    private IEnumerator ReloadAmmo()
    {
        _reloading = true;
        float reloadTime = _steamUpgradeManager.ReloadTime.Current.Value;
        _steamWeaponView.Reload(reloadTime);
        yield return new WaitForSeconds(reloadTime);
        _ammoRemaining = (int)_steamUpgradeManager.ClipSize.Current.Value;
        _reloading = false;
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
