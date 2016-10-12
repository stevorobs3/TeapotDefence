
using UnityEngine;

public class Lid : MonoBehaviour
{
    private float _speed = 5f;

    public delegate void HitEnemyHandler(CoffeeMaker coffeemaker);
    public event HitEnemyHandler HitEnemy;

    void Update()
    {
        Move();
    }


    void Move()
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * _speed, Space.Self);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent == null)
            return;
        CoffeeMaker coffeeMaker = collider.transform.parent.GetComponent<CoffeeMaker>();
        if (coffeeMaker != null)
        {
            Explode(coffeeMaker);
        }
    }

    void Explode(CoffeeMaker coffeemaker)
    {
        // TODO: play die animation  & sound
        if (HitEnemy != null)
            HitEnemy(coffeemaker);
        Destroy(gameObject);
    }
}