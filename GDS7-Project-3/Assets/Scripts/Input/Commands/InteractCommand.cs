using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class InteractCommand : Command
    {
        [SerializeField] private float _maxInteractDistance;
        [SerializeField] private Animator _animator;
        public override void Execute()
        {
            _animator.SetTrigger("Action_Swing");
            if (Physics.Raycast(transform.position, transform.forward, out var hitInfo, _maxInteractDistance))
            {
                var interactable = hitInfo.collider.GetComponent<IInteractable>();
                interactable?.Interact();
            }
        }
    }
}
