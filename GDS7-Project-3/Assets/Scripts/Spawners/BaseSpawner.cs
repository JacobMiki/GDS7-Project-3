using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Spawners
{
    public abstract class BaseSpawner : MonoBehaviour, ISpawner
    {
        protected static Color _gizmosColor = new Color(1f, 0.1f, 0.1f, 0.7f);
        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public abstract GameObject Spawn();

        public void SpawnAndForget()
        {
            Spawn();
        }
    }
}
