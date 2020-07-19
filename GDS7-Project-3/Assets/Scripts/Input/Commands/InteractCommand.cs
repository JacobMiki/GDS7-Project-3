using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class InteractCommand : Command
    {
        [SerializeField] private float _maxInteractDistance;
        [SerializeField] private float _interactCooldown;
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioClip _swingSound;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LayerMask _interactionLayers;

        private ITorchState _torchState;
        private bool _canInteract = true;

        private void Awake()
        {
            _torchState = GetComponent<ITorchState>();
        }

        public override void Execute()
        {
            if (_torchState.HasTorch && _canInteract)
            {
                _canInteract = false;
                StartCoroutine(Cooldown());
                _animator.SetTrigger("Attack");
                _audioSource.PlayOneShot(_swingSound);
                if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, _maxInteractDistance, _interactionLayers))
                {
                    var interactable = hitInfo.collider.GetComponent<IInteractable>();
                    Debug.Log(interactable, this);

                    interactable?.Interact();
                }
            }
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_interactCooldown);
            _canInteract = true;
        }
    }
}
