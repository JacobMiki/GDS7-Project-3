using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS7.Group1.Project3.Assets.Scripts.Triggers
{
    [Serializable]
    public class TriggerEvent : UnityEvent<Collider> { }

    [RequireComponent(typeof(BoxCollider))]
    public class Trigger : MonoBehaviour
    {
        [SerializeField] protected bool _enabled = true;
        [SerializeField] private bool _oneShot;
        [SerializeField] private TriggerEvent _onTrigger = new TriggerEvent();


        private void OnTriggerEnter(Collider other)
        {
            if (!_enabled)
            {
                return;
            }

            if (other.CompareTag("Player"))
            {
                if (HandleTrigger(other))
                {
                    _onTrigger.Invoke(other);
                    if (_oneShot)
                    {
                        _enabled = false;
                        Destroy(gameObject);
                    }
                }
            }
        }

        protected virtual bool HandleTrigger(Collider other)
        {
            return true;
        }


        private static Color _enabledGizmoColor = new Color(1f, 0.9f, 0.3f, 0.4f);
        private static Color _disabledGizmoColor = new Color(1f, 0.9f, 0.3f, 0.05f);
        private void OnDrawGizmos()
        {
            var collider = GetComponent<BoxCollider>();
            Gizmos.color = _enabled ? _enabledGizmoColor : _disabledGizmoColor;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawCube(collider.center, collider.size);
        }
    }
}