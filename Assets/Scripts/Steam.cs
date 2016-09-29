using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Steam: MonoBehaviour
{
    public List<CoffeeMaker> _currentTargets = new List<CoffeeMaker>();

    public float _dps = 5;
    
    TeapotUpgradeManager _teapotUpgradeManager;

    ParticleSystem _particles;

    void Awake()
    {
        _teapotUpgradeManager = FindObjectOfType<TeapotUpgradeManager>();
        _teapotUpgradeManager.RangeUpgraded += UpgradeSteamRange;
        _teapotUpgradeManager.DPSUpgraded += UpgradeSteamAttack;

        _particles = transform.parent.gameObject.GetComponent<ParticleSystem>();
    }

    private void UpgradeSteamAttack(TeapotDPSUpgrade dpsUpgrade)
    {
        _particles.startColor =  dpsUpgrade.Colour;
        _dps = dpsUpgrade.Value;
    }

    private void UpgradeSteamRange(TeapotRangeUpgrade rangeUpgrade)
    {
        _particles.startLifetime = rangeUpgrade.StartLifetime;
        var localScale = transform.localScale;
        localScale.x = rangeUpgrade.XCollider;
        localScale.y = rangeUpgrade.YCollider;
        transform.localScale = localScale;
    }

    void Update()
    {
        var elementsToRemove = new List<CoffeeMaker>();
        foreach (var coffeeMaker in _currentTargets)
        {
            if (coffeeMaker == null || coffeeMaker.TakeDamage(_dps * Time.deltaTime))
                elementsToRemove.Add(coffeeMaker);
        }
        foreach (var element in elementsToRemove)
            _currentTargets.Remove(element);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var coffeeMaker = collider.GetComponent<CoffeeMaker>();
        if (!_currentTargets.Contains(coffeeMaker))
        {
            _currentTargets.Add(coffeeMaker);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        var coffeeMaker = collider.GetComponent<CoffeeMaker>();
        if (_currentTargets.Contains(coffeeMaker))
        {
            _currentTargets.Remove(coffeeMaker);
        }
    }
}

