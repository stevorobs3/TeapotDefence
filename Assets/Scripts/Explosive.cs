using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(ParticleSystem), typeof(SpriteRenderer))]
public class Explosive : MonoBehaviour
{
    
    ParticleSystem _explosion;
    SpriteRenderer _sprite;

    void Awake()
    {
        _explosion = GetComponent<ParticleSystem>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_isExploding)
        {
            _explosionDurationLeft -= Time.deltaTime;
            
            if (_explosionDurationLeft < 0)
            {
                Destroy(gameObject);
            }
            else if (_explosionDurationLeft < _explosionMidpoint)
            {
                DealDamage();
            }
        }
    }

    public int Cost
    {
        get
        {
            return _cost;
        }
    }

    private float _damage = 20;

    private bool _isExploding = false;
    private float _explosionDurationLeft;
    private float _explosionMidpoint;
    private int _cost = 10;

    private List<CoffeeMaker> _coffeeMakers = new List<CoffeeMaker>();


    void OnTriggerEnter2D(Collider2D collision)
    {
        var coffeeMaker = collision.gameObject.GetComponent<CoffeeMaker>();
        if (coffeeMaker != null)
        {
            _coffeeMakers.Add(coffeeMaker);
            if (!_isExploding)
            {
                Debug.Log("Triggering Explosion because of coffeemaker: " + coffeeMaker);
                _isExploding = true;
                Explode();
            }
        }
    }

    private void Explode()
    {
        _sprite.enabled = false;
        _explosionDurationLeft = _explosion.startLifetime + _explosion.duration;
        _explosionMidpoint = _explosion.startLifetime;
        _explosion.Play();      
    }

    private void DealDamage()
    {
        for (int i = 0; i < _coffeeMakers.Count; i++)
        {
            if (_coffeeMakers[i] != null)
                _coffeeMakers[i].TakeDamage(_damage);
        }
    }
}