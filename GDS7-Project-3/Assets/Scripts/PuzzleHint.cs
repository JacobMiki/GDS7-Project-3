using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHint : MonoBehaviour
{
    [SerializeField] private float _minTimeBetweenHints;
    [SerializeField] private float _maxTimeBetweenHints;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private bool _disableTimedHints;

    private Transform _player;
    private bool _canBeDestroyed;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (!_disableTimedHints)
        {
            StartCoroutine(HitAfterWait());
        }
    }

    public void MarkToDestroy()
    {
        _canBeDestroyed = true;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!_renderer.isVisible)
        {
            if (_canBeDestroyed)
            {
                _canBeDestroyed = false;
                Destroy(gameObject);
            }
            var rot = Quaternion.LookRotation(_player.position - transform.position);
            transform.rotation = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        }
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
