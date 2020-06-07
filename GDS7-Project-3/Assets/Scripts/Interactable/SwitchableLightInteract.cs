using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    [RequireComponent(typeof(SwitchableLight))]
    public class SwitchableLightInteract : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _canToggle;
        private SwitchableLight _light;

        private void Awake()
        {
            _light = GetComponent<SwitchableLight>();
        }

        public void Interact()
        {
            if (_canToggle)
            {
                _light.Toggle();
            }
            else
            {
                _light.Switch(true);
            }
        }

    }
}
