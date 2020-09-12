using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;
using UnityEngine.EventSystems;

public class SafeZone : MonoBehaviour
{
    private bool _done = false;

    private void OnTriggerEnter(Collider other)
    {
        TrySetSafeState(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TrySetSafeState(other);
    }

    private void TrySetSafeState(Collider other)
    {
        if (other.CompareTag("Player")
            && !Physics.Linecast(transform.position + Vector3.up, other.transform.position + Vector3.up, LayerMask.GetMask("World"), QueryTriggerInteraction.Ignore))
        {
            var safeState = other.GetComponent<ISafeState>();
            safeState.IsSafe = true;
            if (!_done)
            {
                safeState.LastSafePosition = other.transform.position;
                safeState.LastSafeRotation = other.transform.rotation;
                _done = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var safeState = other.GetComponent<ISafeState>();
            safeState.IsSafe = false;
        }
    }
}
