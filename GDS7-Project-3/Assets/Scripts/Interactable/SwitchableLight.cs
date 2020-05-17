using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    public class SwitchableLight : MonoBehaviour, IInteractable
    {
        [SerializeField] private Light _light;

        public void Awake()
        {
            _light.enabled = false;
        }
        public void Interact()
        {
            _light.enabled = true;
        }

    }
}
