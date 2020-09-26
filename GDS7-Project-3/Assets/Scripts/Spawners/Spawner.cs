using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GDS7.Group1.Project3.Assets.Scripts.Spawners
{
    public class Spawner : BaseSpawner
    {
        [SerializeField] private GameObject _prefab;

        public override GameObject Spawn()
        {
            return Instantiate(_prefab, transform.position, transform.rotation);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
            Handles.color = _gizmosColor;
            Handles.SphereHandleCap(-1, transform.position, Quaternion.identity, 0.3f, EventType.Repaint);
            Handles.color = Color.white;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
        }
#endif
    }
}