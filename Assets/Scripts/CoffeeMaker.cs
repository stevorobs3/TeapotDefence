using UnityEngine;

public class CoffeeMaker : MonoBehaviour
{
    private TeaPlantation _target;

    private SpriteRenderer _healthBar;

    private float speed = 0.5f;
    private float _health = MAX_HEALTH;
    private float? lastAttack;

    const float MAX_HEALTH = 10;
    const float ATTACK_COOLDOWN = 0.5F;
    const int ATTACK_DAMAGE = 5;

    void Start()
    {
        ChooseTarget();
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

    private void ChooseTarget()
    {
        _target = GameObject.FindGameObjectWithTag("TeaPlantation").GetComponent<TeaPlantation>();
    }

    void Update()
    {
        ChooseTarget();
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
        if (lastAttack == null || Time.time - lastAttack > ATTACK_COOLDOWN)
        {
            lastAttack = Time.time;
            if (_target.TakeDamage(ATTACK_DAMAGE))
            {
                ChooseTarget();
            }
        }
    }


    public bool TakeDamage(float amount)
    {
        _health -= amount;
        Debug.Log("Changing scale of " + _healthBar.name);
        _healthBar.transform.localScale = new Vector3((float)_health / MAX_HEALTH, 1, 1);
        var died = _health <= 0;

        if (died)
            Die();

        return died;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}