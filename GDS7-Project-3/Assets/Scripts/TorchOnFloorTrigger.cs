using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class TorchOnFloorTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var torchState = other.GetComponent<ITorchState>();
            if (torchState != null)
            {
                torchState.HasTorch = true;
                Destroy(gameObject);
            }
        }
    }
}