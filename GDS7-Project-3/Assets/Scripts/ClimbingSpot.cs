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

    private static bool _isClimbing = false;

    private void OnTriggerStay(Collider other)
    {
        if (_isClimbing || !other.CompareTag("Player"))
        {
            return;
        }

        var groundedState = other.GetComponent<IGroundedState>();
        if (!groundedState.IsGrounded && Vector3.Angle(other.transform.forward, _target.forward) <= _maxDegreesToTargetForward)
        {
            _isClimbing = true;
            other.GetComponentInChildren<Animator>().SetTrigger("Climb");
            other.transform.rotation = _target.rotation;
            StartCoroutine(MoveToTarget(other.GetComponent<CharacterInput>(), groundedState));
        }
    }

    private IEnumerator MoveToTarget(CharacterInput other, IGroundedState groundedState)
    {
        while (other.InputsEnabled)
        {
            yield return null;
        }

        while (!other.InputsEnabled)
        {
            yield return null;
        }
        other.transform.position = _target.position;

        while (!groundedState.IsGrounded)
        {
            yield return null;
        }
        _isClimbing = false;
    }
}
