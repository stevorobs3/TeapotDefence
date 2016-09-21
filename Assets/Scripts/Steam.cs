using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Steam: MonoBehaviour
{
    public List<CoffeeMaker> _currentTargets = new List<CoffeeMaker>();

    public float DPS = 5;
    private float DPS_BONUS = 5f;


    private List<Color> _upgradeColours = new List<Color>()
    {
        Color.red,
        Color.green,
        Color.blue
    };

    private List<SteamRangeUpgrade> _upgradeRanges = new List<SteamRangeUpgrade>()
    {
        SteamRangeUpgrade.Level1,
        SteamRangeUpgrade.Level2,
        SteamRangeUpgrade.Level3
    };

    class SteamRangeUpgrade
    {
        public static readonly SteamRangeUpgrade Level1 = new SteamRangeUpgrade(0.8f, -1.4f, 1.4f);
        public static readonly SteamRangeUpgrade Level2 = new SteamRangeUpgrade(1.2f, -2f, 1.7f);
        public static readonly SteamRangeUpgrade Level3 = new SteamRangeUpgrade(2f, -3f, 2.7f);

        public readonly float StartLifetime;
        public readonly float XCollider;
        public readonly float YCollider;

        private SteamRangeUpgrade(float startLifetime, float xCollider, float yCollider)
        {
            StartLifetime = startLifetime;
            XCollider = xCollider;
            YCollider = yCollider;
        }
    }

    private int _attackUpgradeLevel = -1;
    private int _rangeUpgradeLevel = -1;


    const int ATTACK_UPGRADE_COST = 20;
    const int RANGE_UPGRADE_COST = 20;


    HotbarManager _hotbarManager;
    CurrencyManager _currencyManager;
    ParticleSystem _particles;

    void Awake()
    {
        _hotbarManager = FindObjectOfType<HotbarManager>();
        _currencyManager = FindObjectOfType<CurrencyManager>();
        _particles = transform.parent.gameObject.GetComponent<ParticleSystem>();
        _hotbarManager.ItemSelected += (selectedItem) => {
            if (selectedItem == SelectedItem.SteamDamageUpgrade && _currencyManager.Spend(ATTACK_UPGRADE_COST * (_attackUpgradeLevel + 2)))
                UpgradeSteamAttack();
            else if (selectedItem == SelectedItem.SteamRangeUpgrade && _currencyManager.Spend(RANGE_UPGRADE_COST * (_rangeUpgradeLevel + 2)))
                UpgradeSteamRange();
        };

    }

    private void UpgradeSteamAttack()
    {
        if (_attackUpgradeLevel < _upgradeColours.Count - 1) {
            _attackUpgradeLevel++;
            _particles.startColor = _upgradeColours[_attackUpgradeLevel];
            DPS += DPS_BONUS;
        }
    }

    private void UpgradeSteamRange()
    {
        if (_rangeUpgradeLevel < _upgradeRanges.Count - 1)
        {
            _rangeUpgradeLevel++;
            var upgradeLevel = _upgradeRanges[_rangeUpgradeLevel];
            _particles.startLifetime = upgradeLevel.StartLifetime;
            var localScale = transform.localScale;
            localScale.x = upgradeLevel.XCollider;
            localScale.y = upgradeLevel.YCollider;
            transform.localScale = localScale;
        }
    }


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

