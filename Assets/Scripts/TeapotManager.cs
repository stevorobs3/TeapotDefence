using UnityEngine;
using System.Collections;

public class TeapotManager : MonoBehaviour {

    public GameObject _teapotPrefab;


    private ParticleSystem _teapotSteam;

    private Vector3 _spawnLocation = new Vector3(0, 1, 0);

    private GameObject _teapot;
    void Awake()
    {
        _teapot = Instantiate(_teapotPrefab, _spawnLocation, Quaternion.identity) as GameObject;
        _teapot.transform.SetParent(gameObject.transform);
        _teapotSteam = _teapot.GetComponentInChildren<ParticleSystem>();
        _teapotSteam.gameObject.SetActive(false);
    }


    void Update ()
    {
        FollowMouse();
        Move();
        FireAttackSteam();
    }

    const float MOVEMENT_SPEED = 1.5f;

    private void FireAttackSteam()
    {
        if (Input.GetMouseButton(0))
        {
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
            deltaY += MOVEMENT_SPEED * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            deltaY -= MOVEMENT_SPEED * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            deltaX -= MOVEMENT_SPEED * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            deltaX += MOVEMENT_SPEED * Time.deltaTime;
        _teapot.transform.position += new Vector3(deltaX, deltaY, 0);
    }

    private void FollowMouse()
    {
        var cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cameraPosition.z = 0;

        var dir = cameraPosition - _teapot.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _teapot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var yScale = (cameraPosition.x > _teapot.transform.position.x) ? 1 : -1;
        _teapot.transform.localScale = new Vector3(-1, yScale, 1);
    }
}
