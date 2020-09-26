using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] private CharacterInput _characterInput;
        [SerializeField] private GameObject _characterModel;

        private PlayerSounds _sounds;
        private ITorchState _torchState;
        private Vector3 _startRootPosition;

        private void Start()
        {
            _sounds = _characterInput.GetComponent<PlayerSounds>();
            _torchState = _characterInput.GetComponent<ITorchState>();
        }

        public void EnableInputs()
        {
            EnableMovement();
            _characterInput.CameraEnabled = true;
        }

        public void DisableInputs()
        {
            DisableMovement();
            _characterInput.CameraEnabled = false;
            _characterInput.LookDelta = Vector2.zero;
        }

        public void EnableMovement()
        {
            _characterInput.InputsEnabled = true;
            _characterInput.GetComponent<Collider>().enabled = true;
            _characterInput.GetComponent<Rigidbody>().isKinematic = false;
            _characterInput.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public void DisableMovement()
        {
            _characterInput.InputsEnabled = false;
            _characterInput.MoveDirection = Vector3.zero;
            _characterInput.GetComponent<Collider>().enabled = false;
            _characterInput.GetComponent<Rigidbody>().isKinematic = true;
            _characterInput.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public void PlayStandUpSound()
        {
            _sounds.Play(PlayerSoundTypes.STAND_UP);
        }

        public void DropTorch()
        {
            _torchState.DropTorch();
        }
    }
}