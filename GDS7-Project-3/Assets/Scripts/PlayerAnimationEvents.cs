using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] private CharacterInput _characterInput;
        [SerializeField] private GameObject _characterModel;

        private PlayerSounds _sounds;
        private Vector3 _startRootPosition;

        private void Start()
        {
            _sounds = _characterInput.GetComponent<PlayerSounds>();
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
            var vec = _characterModel.transform.position - _startRootPosition;
            Debug.Log(vec.x);
            Debug.Log(vec.y);
            Debug.Log(vec.z);
            _characterInput.InputsEnabled = true;
        }

        public void DisableMovement()
        {
            _startRootPosition = _characterModel.transform.position;
            _characterInput.InputsEnabled = false;
            _characterInput.MoveDirection = Vector3.zero;
        }

        public void PlayStandUpSound()
        {
            _sounds.Play(PlayerSoundTypes.STAND_UP);
        }
    }
}