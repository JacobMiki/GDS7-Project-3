using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] private CharacterInput _characterInput;
        [SerializeField] private GameObject _characterModel;
        private Vector3 _startRootPosition;

        private void Start()
        {
        }

        public void EnableInputs()
        {
            _characterInput.InputsEnabled = true;
            _characterInput.CameraEnabled = true;
        }

        public void DisableInputs()
        {
            _characterInput.CameraEnabled = false;
            _characterInput.InputsEnabled = false;
        }

        public void EnableMovement()
        {
            _characterInput.transform.position += GetComponent<Animator>().rootPosition - _startRootPosition;
            _characterInput.GetComponent<Rigidbody>().isKinematic = false;
            _characterInput.InputsEnabled = true;
        }

        public void DisableMovement()
        {
            _characterInput.GetComponent<Rigidbody>().isKinematic = true;
            _characterInput.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _startRootPosition = GetComponent<Animator>().rootPosition;
            _characterInput.InputsEnabled = false;
        }
    }
}