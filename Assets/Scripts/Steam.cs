using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Steam: MonoBehaviour
{
    public List<CoffeeMaker> _currentTargets = new List<CoffeeMaker>();

    public float DPS = 5;

    void Update()
    {
        var elementsToRemove = new List<CoffeeMaker>();
        foreach (var coffeeMaker in _currentTargets)
        {
            if (coffeeMaker.TakeDamage(DPS * Time.deltaTime))
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

