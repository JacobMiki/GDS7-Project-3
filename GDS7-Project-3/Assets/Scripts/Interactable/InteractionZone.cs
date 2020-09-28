using System;
using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Input.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    [System.Serializable]
    public class InteractionZoneEvent : UnityEvent<GameObject, InteractionZone> { }

    [RequireComponent(typeof(Collider))]
    public class InteractionZone : MonoBehaviour
    {
        [SerializeField] private InteractionZoneEvent _onInteract = new InteractionZoneEvent();
        [SerializeField] private InteractionZoneEvent _onEnter = new InteractionZoneEvent();
        [SerializeField] private InteractionZoneEvent _onExit = new InteractionZoneEvent();

        private InteractionGuard _guard;

        public bool IsInteracting { get; set; }

        void OnEnable()
        {
            _guard = GetComponent<InteractionGuard>();
        }

        public void Interact(GameObject interacting)
        {
            if (!_guard || _guard.CanInteract(interacting, this))
            {
                IsInteracting = true;
                StartCoroutine(WaitForInteractionEnd(() =>
                {
                    if (_guard && !_guard.CanInteract(interacting, this))
                    {
                        var cmd = interacting.GetComponent<InteractCommand>();
                        cmd.InteractionZone = null;
                        _onExit.Invoke(interacting, this);
                    }
                }));
                _onInteract.Invoke(interacting, this);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                var cmd = other.GetComponent<InteractCommand>();
                if (cmd && cmd.InteractionZone == null)
                {
                    if (!_guard || _guard.CanInteract(other.gameObject, this))
                    {
                        cmd.InteractionZone = this;
                        _onEnter.Invoke(other.gameObject, this);
                    }
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
                    if (!_guard || _guard.CanInteract(other.gameObject, this))
                    {
                        cmd.InteractionZone = null;
                        _onExit.Invoke(other.gameObject, this);
                    }
                }
            }
        }

        IEnumerator WaitForInteractionEnd(Action callback)
        {
            while (IsInteracting)
            {
                yield return null;
            }
            callback.Invoke();
        }
    }
}