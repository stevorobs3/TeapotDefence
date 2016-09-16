using UnityEngine;

public class CoffeeMaker : MonoBehaviour
{
    private GameObject _target;

    private float speed = 0.5f;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("TeaPlantation");
    }

    void Update()
    {
        var direction = _target.transform.position - transform.position;
        var normalisedDirection = Vector3.Normalize(direction);
        Debug.Log("Target is in direction " + normalisedDirection);

        if (Vector3.Magnitude(normalisedDirection) > 0.5f)
               transform.position += normalisedDirection * speed * Time.deltaTime;
    }
}