
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lid : MonoBehaviour
{
    private float _speed = 5f;
    private float _minTime = 0.5f;
    private float _maxTime = 1.5f;
    private float _blastRadius = 2f;

    public delegate void HitEnemiesHandler(CoffeeMaker[] coffeemakers);
    public event HitEnemiesHandler HitEnemies;


    private Animator _animator;

    public void SetBlastRadius(float blastRadius)
    {
        _blastRadius = blastRadius;
    }

    public void MoveToPosition(Vector3 position)
    {
        float distance = Vector3.Distance(position, transform.position);
        float time = distance / _speed;

        time = Mathf.Min(Mathf.Max(time, _minTime), _maxTime);

        float height = distance * 0.2f;

        transform.DOMove(position, time);
        Sequence mySequence = DOTween.Sequence();

        mySequence.Append(transform.GetChild(0).DOLocalMove(new Vector3(0, height, 0), time/2)).SetEase(Ease.OutSine);
        mySequence.Append(transform.GetChild(0).DOLocalMove(new Vector3(0, 0, 0), time/2)).SetEase(Ease.InSine);
        mySequence.OnComplete(Explode);
    }

    void Explode()
    {
        CoffeeMaker[] coffeeMakers = FindObjectsOfType<CoffeeMaker>();
        List<CoffeeMaker> hitCoffeeMakers = new List<CoffeeMaker>();
        foreach (var coffeeMaker in coffeeMakers)
        {
            if (Vector3.Distance(coffeeMaker.transform.position, transform.position) <= _blastRadius) {
                hitCoffeeMakers.Add(coffeeMaker);
            }
        }

        var explosion = GetComponent<ParticleSystem>();
        explosion.Play();
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        StartCoroutine(Die(explosion.startLifetime + explosion.duration, hitCoffeeMakers.ToArray()));
    }

    IEnumerator Die(float time, CoffeeMaker[] enemies)
    {
        yield return new WaitForSeconds(time);
        if (HitEnemies != null)
            HitEnemies(enemies);

        Destroy(gameObject);
    }
}