using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Spawners
{
    public class Spawner : BaseSpawner
    {
        [SerializeField] private GameObject _prefab;

        public override GameObject Spawn()
        {
            return Instantiate(_prefab, transform.position, transform.rotation);
        }

        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = _gizmosColor;
            Gizmos.DrawSphere(Vector3.zero, 0.3f);
        }
    }
}