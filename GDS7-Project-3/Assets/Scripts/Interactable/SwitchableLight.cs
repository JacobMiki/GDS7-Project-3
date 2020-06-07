using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    public class SwitchableLight : MonoBehaviour
    {
        [SerializeField] private GameObject _light;
        [SerializeField] private bool _activeOnStart = false;
        [SerializeField] private UnityEvent<bool> _onLightSwitch;

        public bool SwitchingDisabled { get; set; }
        public bool IsLightOn { get; private set; }

        public void Awake()
        {
            IsLightOn = _activeOnStart;
            _light.SetActive(_activeOnStart);
        }

        public void Switch(bool on, bool emitEvent = true)
        {
            if (SwitchingDisabled)
            {
                return;
            }

            IsLightOn = on;
            _light.SetActive(IsLightOn);
            if (emitEvent)
            {
                _onLightSwitch?.Invoke(IsLightOn);
            }
        }

        public void Toggle(bool emitEvent = true)
        {
            Switch(!IsLightOn, emitEvent);
        }
    }
}
