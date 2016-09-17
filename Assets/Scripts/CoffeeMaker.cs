using UnityEngine;

public class CoffeeMaker : MonoBehaviour
{
    private TeaPlantation _target;

    private SpriteRenderer _healthBar;

    private float speed = 0.5f;
    private int _health = MAX_HEALTH;
    private float lastAttack = Time.time;

    const int MAX_HEALTH = 10;
    const float ATTACK_COOLDOWN = 0.5F;
    const int ATTACK_DAMAGE = 5;
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("TeaPlantation").GetComponent<TeaPlantation>();
        foreach (Transform child in transform)
        {
            var sprite = child.GetComponent<SpriteRenderer>();
            Debug.Log("Setting health bar as " + child.name);
            if (sprite != null)
            {
                _healthBar = sprite;
            }
        }
    }

    void Update()
    {
        var direction = _target.transform.position - transform.position;
        var normalisedDirection = Vector3.Normalize(direction);

        if (Vector3.Magnitude(direction) > 0.5f)
            transform.position += normalisedDirection * speed * Time.deltaTime;
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttack > ATTACK_COOLDOWN)
        {
            _target.TakeDamage(ATTACK_DAMAGE);
        }
    }


    public void TakeDamage(int amount)
    {
        _health -= amount;
        Debug.Log("Changing scale of " + _healthBar.name);
        _healthBar.transform.localScale = new Vector3((float)_health / MAX_HEALTH,1,1);

        if (_health <= 0)
            Die();
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}