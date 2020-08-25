using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace GDS7.Group1.Project3.Assets.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private bool _activateOnStart;
        [SerializeField] private float _deathAnimTime;
        [SerializeField] private GameObject _killZone;
        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private UnityEvent _onDefeat = new UnityEvent();

        private bool _active = false;
        private GameObject _player;

        void Start()
        {
            _agent.enabled = false;
            if (NavMesh.SamplePosition(transform.position, out var closestHit, 100f, NavMesh.AllAreas))
            {
                transform.position = closestHit.position;
                _agent.enabled = true;

            }

            if (_activateOnStart)
            {
                Activate();
            }
        }

        public void Activate()
        {
            _player = GameObject.Find("Player");
            _active = true;
            _animator.SetBool("IsRunning", true);
            FindTarget();
        }

        public void Deactivate()
        {
            _active = false;
            _animator.SetBool("IsRunning", false);
            _agent.destination = transform.position;
        }

        public void Defeat()
        {
            _active = false;
            _killZone.SetActive(false);
            _onDefeat.Invoke();
            _animator.SetTrigger("Die");
            _deathParticles.SetActive(true);
            _agent.destination = transform.position;
            Destroy(gameObject, _deathAnimTime);
        }

        private void FindTarget()
        {
            if (_player)
            {
                _agent.destination = _player.transform.position;
            }
        }


        void Update()
        {
            if (_active && _agent.isActiveAndEnabled && _agent.isOnNavMesh)
            {
                FindTarget();
            }
        }
    }
}