﻿using System.Linq;
using UnityEngine;

public class CoffeeMaker : MonoBehaviour
{
    private Plantation _target;

    private SpriteRenderer _healthBar;

    private GameController _gameController;

    
    private float _health;

    public float speed = 0.5f;
    public float _maxHealth = 10;
    public float _attackDamagePerSecond = 3;

    void Start()
    {

        _health = _maxHealth;
        _gameController = FindObjectOfType<GameController>();
        ChooseTarget();
        foreach (Transform child in transform)
        {
            var sprite = child.GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                _healthBar = sprite;
            }
        }
    }

    private void ChooseTarget()
    {
        var targets = FindObjectsOfType<Plantation>();
        if (targets.Length == 0)
        {
            _target = null;
            _gameController.EndGame();
            return;
        }
        _target = targets.First();
        foreach (var target in targets)
        {
            var distanceToTarget        = Vector3.Magnitude(transform.position - target.transform.position);
            var distanceToCurrentTarget = Vector3.Magnitude(transform.position - _target.transform.position);
            if (distanceToTarget < distanceToCurrentTarget)
            {
                _target = target;
            }
        }        
    }

    void Update()
    {
        ChooseTarget();
        if (_target == null)
            return;

        var direction = _target.transform.position - transform.position;
        var normalisedDirection = Vector3.Normalize(direction);

        if (Vector3.Magnitude(direction) > 0.5f)
        {
            transform.position += normalisedDirection * speed * Time.deltaTime;
            var go = gameObject;
            TransformHelper.LookAtTarget(_target.transform.position, ref go);
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (_target.TakeDamage(_attackDamagePerSecond * Time.deltaTime))
        {
            ChooseTarget();
        }
    }


    public bool TakeDamage(float amount)
    {
        _health -= amount;
        var localScale = _healthBar.transform.localScale;
        localScale.x = (float)_health / _maxHealth;
        _healthBar.transform.localScale = localScale;

        var died = _health <= 0;

        if (died)
            Die();

        return died;
    }
    
    private void Die()
    {
        _gameController.CafetieresKilled(1);
        Destroy(gameObject);
    }
}