using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class PlayerAnimationEvents : MonoBehaviour
    {
        [SerializeField] private CharacterInput _characterInput;

        public void EnableInputs()
        {
            Debug.Log("Enabling inputs", this);
            _characterInput.InputsEnabled = true;
            _characterInput.CameraEnabled = true;
        }

        public void DisableInputs()
        {
            Debug.Log("Disabling inputs", this);
            _characterInput.CameraEnabled = false;
            _characterInput.InputsEnabled = false;
        }
    }
}