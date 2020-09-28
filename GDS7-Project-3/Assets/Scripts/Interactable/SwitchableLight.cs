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
        [SerializeField] private float _timeToLight;

        public bool SwitchingDisabled { get; set; }
        public bool IsLightOn { get; private set; }

        public void Awake()
        {
            IsLightOn = _activeOnStart;
            _light.SetActive(_activeOnStart);
        }

        public void Switch(bool on)
        {
            Switch(on, true);
        }

        public void Switch(bool on, bool emitEvent = true, bool instant = false, float addTime = 0f)
        {
            if (SwitchingDisabled || IsLightOn == on)
            {
                return;
            }

            IsLightOn = on;
            if (instant)
            {
                _light.SetActive(IsLightOn);
            }
            else
            {
                StartCoroutine(Light(on, addTime));
            }

            if (emitEvent)
            {
                _onLightSwitch?.Invoke(IsLightOn);
            }
        }

        public void Toggle(bool emitEvent = true, bool instant = false, float addTime = 0f)
        {
            Switch(!IsLightOn, emitEvent, instant, addTime);
        }

        IEnumerator Light(bool on, float addTime)
        {
            yield return new WaitForSeconds(_timeToLight + addTime);
            _light.SetActive(IsLightOn);
        }
    }
}
