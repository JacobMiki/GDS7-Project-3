using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") 
            && !Physics.Raycast(transform.position, 
            other.transform.position - transform.position, 
            Vector3.Distance(transform.position, other.transform.position), 
            LayerMask.GetMask("World")))
        {
            var safeState = other.GetComponent<ISafeState>();
            safeState.IsSafe = true;
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
