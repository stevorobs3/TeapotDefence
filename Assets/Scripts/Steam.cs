using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Steam: MonoBehaviour
{
    public int DAMAGE = 5;
    void OnTriggerEnter2D(Collider2D collider)
    {
        var coffeeMaker = collider.GetComponent<CoffeeMaker>();
        if (coffeeMaker != null)
        {
            coffeeMaker.TakeDamage(DAMAGE);
        }
    }
}

