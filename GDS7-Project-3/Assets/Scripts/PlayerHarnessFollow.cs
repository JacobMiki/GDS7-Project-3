using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarnessFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _smoothing;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _transform.position = Vector3.Lerp(
            _transform.position,
            _playerTransform.position,
            _smoothing * Time.fixedDeltaTime
        );
    }
}
