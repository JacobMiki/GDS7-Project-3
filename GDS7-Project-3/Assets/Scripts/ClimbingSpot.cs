using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

public class ClimbingSpot : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _targetTolerance;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxDegreesToTargetForward;

    private bool _isClimbing = false;

    private void OnTriggerStay(Collider other)
    {
        if (_isClimbing || !other.CompareTag("Player"))
        {
            return;
        }

        if (!other.GetComponent<IGroundedState>().IsGrounded && Vector3.Angle(other.transform.forward, _target.forward) <= _maxDegreesToTargetForward)
        {
            _isClimbing = true;
            other.GetComponent<CharacterInput>().InputsEnabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<Collider>().enabled = false;
            StartCoroutine(MoveToTarget(other.transform));
        }
    }

    private IEnumerator MoveToTarget(Transform other)
    {
        while(Vector3.Distance(other.position, _target.position) > _targetTolerance)
        {
            other.position = Vector3.Slerp(other.position, _target.position, Time.deltaTime * _moveSpeed);
            yield return null;
        }
        other.GetComponent<CharacterInput>().InputsEnabled = true;
        other.GetComponent<Rigidbody>().isKinematic = false;
        other.GetComponent<Collider>().enabled = true;
        _isClimbing = false;
    }
}
