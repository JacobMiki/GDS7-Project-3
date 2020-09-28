using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

public class ClimbingSpot : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _maxDegreesToTargetForward;

    [SerializeField] private Vector3? _targetPoint;
    [SerializeField] private Vector3 _climbAnimVector;


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
            _targetPoint = GetTargetPoint(other);

            if (_targetPoint.HasValue)
            {
                _target.position = _targetPoint.Value;

                _isClimbing = true;
                StartCoroutine(Climb(other.GetComponent<CharacterInput>(), groundedState));
            }
        }
    }

    private Vector3? GetTargetPoint(Collider other)
    {
        var surface = transform.position;
        if (Physics.Raycast(_target.position + Vector3.up * 0.3f, Vector3.down, out var surfaceHit, 0.5f, LayerMask.GetMask("World"), QueryTriggerInteraction.Ignore))
        {
            surface = surfaceHit.point + Vector3.up * 0.05f;
        }

        var side = other.transform.position;
        var sideRay = new Ray(other.transform.position + other.transform.forward * -0.3f, other.transform.forward);
        Debug.DrawRay(sideRay.origin, sideRay.direction, Color.magenta, 5f);
        if (Physics.Raycast(sideRay, out var sideHit, 0.7f, LayerMask.GetMask("World"), QueryTriggerInteraction.Ignore))
        {
            side = sideHit.point;
        } 
        else
        {
            return null;
        }

        var cornerSide = new Vector3(side.x, surface.y, side.z);
        var surfaceToCornerSide = cornerSide - surface;
        var surfaceToCorner = Vector3.Project(surfaceToCornerSide, _target.forward);
        var corner = surface + surfaceToCorner;

        Debug.DrawLine(surface, cornerSide, Color.green, 5f);
        Debug.DrawLine(side, cornerSide, Color.red, 5f);
        Debug.DrawLine(surface, corner, Color.blue, 5f);

        return corner + _target.forward * 0.15f;

    }

    private IEnumerator Climb(CharacterInput other, IGroundedState groundedState)
    {
        other.GetComponentInChildren<Animator>().SetTrigger("Climb");
        other.GetComponentInChildren<Animator>().SetBool("IsClimbing", true);

        other.GetComponent<PlayerSounds>().Play(PlayerSoundTypes.CLIMB);

        other.transform.rotation = _target.rotation;

        var targetPoint = _targetPoint.Value;

        other.GetComponent<Collider>().enabled = false;
        other.GetComponent<Rigidbody>().isKinematic = true;
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.InputsEnabled = false;

        while (!other.InputsEnabled)
        {
            other.transform.position = targetPoint - GetLocalTranslateVector();
            yield return null;
        }

        other.transform.position = targetPoint;
        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.GetComponent<Rigidbody>().isKinematic = false;
        other.GetComponent<Collider>().enabled = true;

        while (!groundedState.IsGrounded)
        {
            yield return null;
        }

        _isClimbing = false;
        other.GetComponentInChildren<Animator>().SetFloat("DistanceFromGround", 0f);
        other.GetComponentInChildren<Animator>().SetBool("IsClimbing", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(_target.position + Vector3.up * 0.3f, _target.position);
    }

    private Vector3 GetLocalTranslateVector()
    {
        return _target.forward * _climbAnimVector.z + _target.up * _climbAnimVector.y + _target.right * _climbAnimVector.x;
    }
}
