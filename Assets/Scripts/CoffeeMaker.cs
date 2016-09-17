using UnityEngine;

public class CoffeeMaker : MonoBehaviour
{
    private GameObject _target;

    private float speed = 0.5f;
    private int health = 10;

    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("TeaPlantation");
    }

    void Update()
    {
        var direction = _target.transform.position - transform.position;
        var normalisedDirection = Vector3.Normalize(direction);

        if (Vector3.Magnitude(normalisedDirection) > 0.5f)
               transform.position += normalisedDirection * speed * Time.deltaTime;
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}