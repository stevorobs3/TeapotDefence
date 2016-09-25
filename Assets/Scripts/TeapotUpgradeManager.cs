﻿
public class TeapotUpgradeManager<T> where T : TeapotUpgrade
{
    public TeapotUpgradeManager(T[] upgrades)
    {
        _upgrades = upgrades;
    }
    private int _currentIndex = 0;
    private T[] _upgrades;

    public T Current { get { return _upgrades[_currentIndex]; } }
    public T Next
    {
        get
        {
            if (_currentIndex == _upgrades.Length - 1)
                return null;
            else
                return _upgrades[_currentIndex + 1];
        }
    }

    public bool CanUpgrade()
    {
        return _currentIndex < _upgrades.Length - 1;
    }

    public bool Upgrade()
    {
        bool canUpgrade = CanUpgrade();
        if (canUpgrade)
        {
            _currentIndex++;
        }
        return canUpgrade;
    }
}