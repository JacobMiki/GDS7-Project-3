using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    [RequireComponent(typeof(SwitchableLight))]
    public class SwitchableLightInteract : MonoBehaviour
    {
        [SerializeField] private bool _canToggle;
        [SerializeField] private float _animationTime;
        [SerializeField] private float _timeToLight = 1f;
        [SerializeField] private Transform _animationSnapPoint;

        private SwitchableLight _light;

        void OnEnable()
        {
            _light = GetComponent<SwitchableLight>();
        }

        public void Interact(GameObject interacting, InteractionZone zone)
        {
            StartCoroutine(Light());
            var animator = interacting.GetComponentInChildren<Animator>();
            if (animator)
            {
                interacting.GetComponent<Rigidbody>().velocity = Vector3.zero;
                interacting.transform.position = _animationSnapPoint.position;
                interacting.transform.rotation = _animationSnapPoint.rotation;
                animator.SetTrigger("Light Up");
            }
            StartCoroutine(WaitToEndInteraction(zone));
        }

        IEnumerator WaitToEndInteraction(InteractionZone zone)
        {
            yield return new WaitForSeconds(_animationTime);
            zone.IsInteracting = false;
        }

        IEnumerator Light()
        {
            yield return new WaitForSeconds(_timeToLight);
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
