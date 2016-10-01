using UnityEngine;
using System.Collections;

public class TeapotManager : MonoBehaviour {

    public GameObject _teapotPrefab;


    private ParticleSystem _teapotSteam;
    private GameController _gameController;

    private HotbarManager _hotbarManager;

    private TeapotUpgradeManager _upgradeManager;

    private Vector3 _spawnLocation = new Vector3(0, 2, 0);

    const float STEAM_UNITS_PER_SECOND = 2F;

    public GameObject Teapot;
    void Awake()
    {
        _upgradeManager = FindObjectOfType<TeapotUpgradeManager>();
        _gameController = FindObjectOfType<GameController>();
        _hotbarManager = FindObjectOfType<HotbarManager>();
        Teapot = Instantiate(_teapotPrefab, _spawnLocation, Quaternion.identity) as GameObject;
        Teapot.transform.SetParent(gameObject.transform);
        _teapotSteam = Teapot.GetComponentInChildren<ParticleSystem>();
        _teapotSteam.gameObject.SetActive(false);

        _upgradeManager.SpeedUpgraded += UpgradeSpeed;
    }

    void UpgradeSpeed(TeapotSpeedUpgrade speedUpgrade)
    {
        _speed = speedUpgrade.Value;
    }

    void Update ()
    {
        FollowMouse();
        Move();
        FireAttackSteam();
    }

    private float _speed = 1.5f;

    private void FireAttackSteam()
    {
        if (Input.GetMouseButton(0) && _hotbarManager.CurrentlySelectedItem() == SelectedItem.Steam)
        {
            _gameController.SteamUsed(STEAM_UNITS_PER_SECOND * Time.deltaTime);
            if (!_teapotSteam.isPlaying)
            {
                _teapotSteam.gameObject.SetActive(true);
                _teapotSteam.Play();
            }
        }
        else
        {
            _teapotSteam.gameObject.SetActive(false);
            _teapotSteam.Stop();
        }
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
        Teapot.transform.position += new Vector3(deltaX, deltaY, 0);
        EnsureWithinGrid();

    }

    void EnsureWithinGrid()
    {
        var position = Teapot.transform.position;
        Teapot.transform.position = CoffeeMakerSpawner.ForceWithinGrid(position);
    }

    private void FollowMouse()
    {
        var cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cameraPosition.z = 0;

        TransformHelper.LookAtTarget(cameraPosition, ref Teapot);
        var yScale = (cameraPosition.x > Teapot.transform.position.x) ? 1 : -1;
        var localScale = _teapotSteam.gameObject.transform.GetChild(0).localScale;
        if (yScale * localScale.y < 0)
        {
            localScale.y *= -1;
            _teapotSteam.gameObject.transform.GetChild(0).localScale = localScale;
        }
    }
}
