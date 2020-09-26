using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Input.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    [System.Serializable]
    public class InteractionZoneOnInteractEvent : UnityEvent<GameObject, InteractionZone> { }

    [RequireComponent(typeof(Collider))]
    public class InteractionZone : MonoBehaviour
    {
        [SerializeField] private InteractionZoneOnInteractEvent _onInteract = new InteractionZoneOnInteractEvent();

        public bool IsInteracting { get; set; }

        public void Interact(GameObject interacting)
        {
            IsInteracting = true;
            _onInteract.Invoke(interacting, this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                var cmd = other.GetComponent<InteractCommand>();
                if (cmd)
                {
                    cmd.InteractionZone = this;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var cmd = other.GetComponent<InteractCommand>();
                if (cmd && cmd.InteractionZone == this)
                {
                    cmd.InteractionZone = null;
                }
            }
        }
    }
}