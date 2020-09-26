using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Numerics;
#if UNITY_EDITOR
using UnityEditor;
#endif

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


#if UNITY_EDITOR
        private static Color _enabledGizmoColor = new Color(1f, 0.9f, 0.3f, 0.4f);
        private static Color _disabledGizmoColor = new Color(1f, 0.9f, 0.3f, 0.05f);
        private void OnDrawGizmos()
        {
            var collider = GetComponent<BoxCollider>();
            Handles.color = _enabled ? _enabledGizmoColor : _disabledGizmoColor;
            Handles.matrix = transform.localToWorldMatrix;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;

            Handles.DrawWireCube(collider.center, collider.size);

            Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
            Handles.color = Color.white;
            Handles.matrix = UnityEngine.Matrix4x4.identity;
        }
#endif
    }
}