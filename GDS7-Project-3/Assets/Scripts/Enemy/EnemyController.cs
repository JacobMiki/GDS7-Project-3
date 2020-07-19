using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GDS7.Group1.Project3.Assets.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        // Start is called before the first frame update
        void Start()
        {
            _agent.enabled = false;
            if (NavMesh.SamplePosition(transform.position, out var closestHit, 100f, NavMesh.AllAreas))
            {
                transform.position = closestHit.position;
                _agent.enabled = true;

            }

        }

        // Update is called once per frame
        void Update()
        {
            if (_agent.isActiveAndEnabled && _agent.isOnNavMesh)
            {
                var player = GameObject.Find("Player");
                if (player)
                {
                    _agent.destination = player.transform.position;
                }
            }
        }
    }
}