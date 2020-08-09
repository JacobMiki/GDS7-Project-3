using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHint : MonoBehaviour
{
    [SerializeField] private float _minTimeBetweenHints;
    [SerializeField] private float _maxTimeBetweenHints;
    [SerializeField] private AudioSource _audioSource;
    void Start()
    {
        StartCoroutine(HitAfterWait());
    }

    private IEnumerator HitAfterWait()
    {
        yield return new WaitForSeconds(Random.Range(_minTimeBetweenHints, _maxTimeBetweenHints));
        _audioSource.Play();
        StartCoroutine(HitAfterWait());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
