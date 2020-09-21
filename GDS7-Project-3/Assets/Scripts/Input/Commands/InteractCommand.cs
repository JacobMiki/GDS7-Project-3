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
        private bool _canInteract = true;

        private void Awake()
        {
            _torchState = GetComponent<ITorchState>();
            _sounds = GetComponent<PlayerSounds>();
        }

        public override void Execute()
        {
            if (_torchState.HasTorch && _canInteract)
            {
                _canInteract = false;
                StartCoroutine(Cooldown());
                _sounds.Play(PlayerSoundTypes.SWING);
                _torchInteraction.StartSwing();
            }
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(_interactCooldown);
            _canInteract = true;
            _torchInteraction.EndSwing();
        }
    }
}
