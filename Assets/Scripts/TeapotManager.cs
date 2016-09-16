using UnityEngine;
using System.Collections;

public class TeapotManager : MonoBehaviour {

    public GameObject _teapotPrefab;
    public GameObject _teapotSteamPrefab;


    private GameObject _teapot;
    void Awake()
    {
        _teapot = Instantiate(_teapotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        _teapot.transform.SetParent(gameObject.transform);
        
    }


    void Update ()
    {
        FollowMouse();
        Move();
        FireWeapon();
    }

    const float MOVEMENT_SPEED = 0.05f;
    const float COOLDOWN = 0.333f;

    float? lastFire;

    private void FireWeapon()
    {
        if (Input.GetMouseButton(0) && (lastFire == null || (Time.time - lastFire) > COOLDOWN))
        {
            FireAttackSteam();
            lastFire = Time.time;
        }
    }

    private void FireAttackSteam()
    {
        var go = Instantiate(_teapotSteamPrefab, _teapot.transform) as GameObject;
        go.transform.SetParent(transform, true);
        go.transform.position = _teapot.transform.position;
        go.transform.rotation = _teapot.transform.rotation;
        var particles = go.GetComponent<ParticleSystem>();
        particles.Play();
        Destroy(go, particles.duration + particles.startLifetime);
    }

    private void Move()
    {
        float deltaX = 0, deltaY = 0;
        if (Input.GetKey(KeyCode.W))
            deltaY += MOVEMENT_SPEED;
        if (Input.GetKey(KeyCode.S))
            deltaY -= MOVEMENT_SPEED;
        if (Input.GetKey(KeyCode.A))
            deltaX -= MOVEMENT_SPEED;
        if (Input.GetKey(KeyCode.D))
            deltaX += MOVEMENT_SPEED;
        _teapot.transform.position += new Vector3(deltaX, deltaY, 0);
    }

    private void FollowMouse()
    {
        var cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cameraPosition.z = 0;

        var dir = cameraPosition - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        _teapot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var yScale = (cameraPosition.x > 0) ? 1 : -1;
        _teapot.transform.localScale = new Vector3(-1, yScale, 1);
    }
}
