using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GDS7.Group1.Project3.Assets.Scripts.Spawners
{
    public enum SpawnGroupMode
    {
        RANDOM,
        CLOSEST,
        FURTHEST,
        ALL
    }
    class SpawnGroup : BaseSpawner
    {
        [SerializeField] private SpawnGroupMode _spawnGroupMode = SpawnGroupMode.RANDOM;

        public override GameObject Spawn()
        {
            if (!GetSpawners().Any())
            {
                return null;
            }

            var spawners = GetSpawners();
            var player = GameObject.FindGameObjectWithTag("Player");

            switch (_spawnGroupMode)
            {
                case SpawnGroupMode.RANDOM:
                    var spawnersArray = spawners.ToArray();
                    return spawnersArray[Random.Range(0, spawnersArray.Length)].Spawn();
                case SpawnGroupMode.CLOSEST:
                    var closest = spawners.First();
                    var minDist = Mathf.Infinity;
                    foreach(var spawner in spawners)
                    {
                        var dist = Vector3.Distance(player.transform.position, spawner.GetPosition());
                        if (dist < minDist)
                        {
                            minDist = dist;
                            closest = spawner;
                        }
                    }
                    return closest.Spawn();
                case SpawnGroupMode.FURTHEST:
                    var furthest = spawners.First();
                    var maxDist = 0f;
                    foreach (var spawner in spawners)
                    {
                        var dist = Vector3.Distance(player.transform.position, spawner.GetPosition());
                        if (dist > maxDist)
                        {
                            maxDist = dist;
                            furthest = spawner;
                        }
                    }
                    return furthest.Spawn();
                case SpawnGroupMode.ALL:
                    foreach (var spawner in spawners)
                    {
                        spawner.Spawn();
                    }
                    return null;
            }

            return null;
        }

        private IEnumerable<ISpawner> GetSpawners()
        {
            foreach(Transform child in gameObject.transform)
            {
                if (child.gameObject.activeInHierarchy)
                {
                    var spawner = child.GetComponent<ISpawner>();
                    if (spawner != null)
                    {
                        yield return spawner;
                    }
                }
            }
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!GetSpawners().Any())
            {
                return;
            }

            var spawners = GetSpawners();
            var first = spawners.First();

            Handles.color = _gizmosColor;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.LessEqual;
            Handles.CubeHandleCap(-1, transform.position, Quaternion.identity, 0.3f, EventType.Repaint);
            Handles.DrawAAPolyLine(1f, transform.position, first.GetPosition());
            foreach (var spawner in spawners)
            {
                Handles.DrawAAPolyLine(1f, transform.position, spawner.GetPosition());
            }
            Handles.color = Color.white;
            Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
        }
#endif
    }
}
