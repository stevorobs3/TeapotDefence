using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponView : MonoBehaviour {
    public Slider _reloadSlider;
    public Slider _ammoSlider;

    private int _maxAmmo;
    private int _ammoRemaining;

    private float _reloadTime;
    
    void Awake()
    {
        SetAmmoSlider(_ammoSlider, 1, 1);
        SetAmmoSlider(_reloadSlider, 1, 1);
    }

    public void Reload(float reloadTime)
    {
        StartCoroutine(ReloadInternal(reloadTime));
    }

    public void SetClipSize(int amount)
    {
        _maxAmmo = amount;
        _ammoRemaining = _maxAmmo;
    }

    public void SetReloadTime(float reloadTime)
    {
        _reloadTime = reloadTime;
    }

    public void UseBullet()
    {
        _ammoRemaining--;
        SetAmmoSlider(_ammoSlider, _ammoRemaining, _maxAmmo);
    }

    private IEnumerator ReloadInternal(float reloadTime)
    {
        float currentTime = 0;
        while (currentTime < reloadTime)
        {
            yield return null;
            currentTime += Time.deltaTime;
            _reloadSlider.value = currentTime / reloadTime;
        }
        _reloadSlider.value = 1;
        _ammoRemaining = _maxAmmo;
        SetAmmoSlider(_ammoSlider, _maxAmmo, _maxAmmo);
    }

    private void SetAmmoSlider(Slider slider, float value, float maxValue)
    {
        slider.value = value / maxValue;
    }
}