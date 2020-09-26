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
        [SerializeField] private float _interactCooldown;
        [SerializeField] private TorchInteraction _torchInteraction;

        private ITorchState _torchState;
        private PlayerSounds _sounds;

        public InteractionZone InteractionZone { get; set; }

        private void Awake()
        {
            _torchState = GetComponent<ITorchState>();
            _sounds = GetComponent<PlayerSounds>();
        }

        public override void Execute()
        {
            if (InteractionZone)
            {
                if (InteractionZone.IsInteracting)
                {
                    return;
                }

                InteractionZone.Interact(gameObject);
                return;
            }

            if (_torchState.HasTorch)
            {
                if (_torchInteraction.isSwinging)
                {
                    return;
                }

                StartCoroutine(Cooldown());
                _sounds.Play(PlayerSoundTypes.SWING);
                _torchInteraction.StartSwing();
                return;
            }
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_interactCooldown);
            if (_torchInteraction.isSwinging)
            {
                _torchInteraction.EndSwing();
            }
        }
    }
}
