using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarnessFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _smoothing;
    [SerializeField] private float _cameraHeight;
    [SerializeField] private float _cameraDistance;
    [SerializeField] private LayerMask _cameraOccluders;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        _transform.position = Vector3.Lerp(
            _transform.position,
            new Vector3(_playerTransform.position.x , _playerTransform.position.y + _cameraHeight, _playerTransform.position.z),
            _smoothing * Time.fixedDeltaTime
        );

        var newCameraPos = _transform.position + _transform.forward * -_cameraDistance;


        if (Physics.Linecast(transform.position, newCameraPos, out var wallHit, _cameraOccluders))
        {
            newCameraPos = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, _camera.position.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }

        _camera.position = Vector3.Lerp(_camera.position, newCameraPos, _smoothing * Time.fixedDeltaTime);

    }
}
