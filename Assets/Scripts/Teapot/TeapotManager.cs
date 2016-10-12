using UnityEngine;
using System.Collections;

public class TeapotManager : MonoBehaviour {

    public GameObject _teapotPrefab;
    
    private float _speed = 1.5f;

    public Steam _steamPrefab;
    public SteamUpgradeManager _steamUpgradeManager;
    private int _steamAmmoRemaining;
    private bool _steamReloading;
    public WeaponView _steamWeaponView;


    public Lid _lidPrefab;
    private LidUpgradeManager _lidUpgradeManager;
    private int _lidAmmoRemaining;
    private bool _lidReloading;
    public WeaponView _lidWeaponView;

    private Vector3 _spawnLocation = new Vector3(2, 2, 0);
    private Teapot _teapot;

    private bool _shopIsVisible = false;

    void Awake()
    {
        _steamUpgradeManager = FindObjectOfType<SteamUpgradeManager>();
        _lidUpgradeManager = FindObjectOfType<LidUpgradeManager>();
        _teapot = (Instantiate(_teapotPrefab, _spawnLocation, Quaternion.identity)as GameObject).GetComponent<Teapot>();
        _teapot.transform.SetParent(gameObject.transform);


        _steamUpgradeManager.Initialized += () =>
        {
            float currentReloadTime = _steamUpgradeManager.ReloadTime.Current.Value;
            _steamAmmoRemaining = (int)_steamUpgradeManager.ClipSize.Current.Value;

            _steamWeaponView.SetClipSize(_steamAmmoRemaining);

            _steamUpgradeManager.ClipSizeUpgraded += (clipSize) => _steamWeaponView.SetClipSize((int)clipSize.Value);
        };

        _lidUpgradeManager.Initialized += () =>
        {
            float currentReloadTime = _lidUpgradeManager.ReloadTime.Current.Value;
            _lidAmmoRemaining = (int)_lidUpgradeManager.ClipSize.Current.Value;

            _lidWeaponView.SetClipSize(_lidAmmoRemaining);

            _lidUpgradeManager.ClipSizeUpgraded += (clipSize) => _lidWeaponView.SetClipSize((int)clipSize.Value);
        };
    }

    void Update ()
    {
        var go = _teapot.gameObject;
        FollowMouse(ref go);
        Move();
        FireAttackSteam();
        FireAttackLid();
        LookForReload();
    }

    void LookForReload()
    {
        if (Input.GetKeyDown(KeyCode.R) && _steamAmmoRemaining < (int)_steamUpgradeManager.ClipSize.Current.Value && !_steamReloading)
        {
            StartCoroutine(ReloadSteamAmmo());
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
        if (Input.GetMouseButtonDown(0) && _steamAmmoRemaining > 0 && !_steamReloading && !_shopIsVisible)
        {
            _steamAmmoRemaining--;
            _steamWeaponView.UseBullet();
            if (_steamAmmoRemaining <= 0)
            {
                StartCoroutine(ReloadSteamAmmo());
            }

            Steam steam = GameObject.Instantiate(_steamPrefab) as Steam;
            steam.transform.position = _teapot.Nose.transform.position;
            var go = steam.gameObject;
            FollowMouse(ref go);
            go.transform.Rotate(new Vector3(0, 180, 0));
            steam.HitEnemy += (enemy) => AttackEnemy(enemy, _steamUpgradeManager.Damage.Current.Value);
        }
    }

    private void FireAttackLid()
    {
        if (Input.GetMouseButtonDown(1) && _lidAmmoRemaining > 0 && !_lidReloading && !_shopIsVisible)
        {
            _lidAmmoRemaining--;
            _lidWeaponView.UseBullet();
            if (_lidAmmoRemaining <= 0)
            {
                StartCoroutine(ReloadLidAmmo());
            }

            Lid lid = GameObject.Instantiate(_lidPrefab) as Lid;
            lid.transform.position = _teapot.Lid.transform.position;
            var go = lid.gameObject;
            FollowMouse(ref go);
            go.transform.Rotate(new Vector3(0, 180, 0));
            lid.HitEnemy += (enemy) => AttackEnemy(enemy, _lidUpgradeManager.Damage.Current.Value);
        }
    }

    private IEnumerator ReloadSteamAmmo()
    {
        _steamReloading = true;
        float reloadTime = _steamUpgradeManager.ReloadTime.Current.Value;
        _steamWeaponView.Reload(reloadTime);
        yield return new WaitForSeconds(reloadTime);
        _steamAmmoRemaining = (int)_steamUpgradeManager.ClipSize.Current.Value;
        _steamReloading = false;
    }

    private IEnumerator ReloadLidAmmo()
    {
        _lidReloading = true;
        float reloadTime = _lidUpgradeManager.ReloadTime.Current.Value;
        _lidWeaponView.Reload(reloadTime);
        yield return new WaitForSeconds(reloadTime);
        _lidAmmoRemaining = (int)_lidUpgradeManager.ClipSize.Current.Value;
        _lidReloading = false;
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
