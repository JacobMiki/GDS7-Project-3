using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class TorchOnFloorTrigger : MonoBehaviour
    {
        public void PickUpTorch(GameObject picker, InteractionZone zone = null)
        {
            var torchState = picker.GetComponent<ITorchState>();
            if (torchState != null)
            {
                torchState.HasTorch = true;
                Destroy(gameObject);
            }
            zone.IsInteracting = false;
        }
    }
}