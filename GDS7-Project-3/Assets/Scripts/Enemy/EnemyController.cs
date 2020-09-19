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
        [SerializeField] private GameObject _model;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private bool _activateOnStart;
        [SerializeField] private float _deathAnimTime;
        [SerializeField] private float _fullDeathTime;
        [SerializeField] private float _deathSpeed;
        [SerializeField] private GameObject _killZone;
        [SerializeField] private GameObject _affectTorchArea;
        [SerializeField] private GameObject _deathParticles;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private float _onHitForce;

        [SerializeField] private UnityEvent _onDefeat = new UnityEvent();

        private bool _active = false;
        private bool _dead = false;
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
            _dead = true;
            _killZone.SetActive(false);
            // Destroy(_affectTorchArea);
            _onDefeat.Invoke();
            _animator.SetTrigger("Die");
            _deathParticles.SetActive(true);
            _agent.enabled = false;
            Destroy(_model, _deathAnimTime);
            Destroy(gameObject, _fullDeathTime);
        }

        public void Hit()
        {
            _agent.velocity = -_agent.transform.forward * _onHitForce;
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
            if (_dead)
            {
                _audio.volume *= 0.5f;
                // transform.position += Vector3.up * _deathSpeed * Time.deltaTime;
                transform.position += -transform.forward * _deathSpeed * Time.deltaTime;
            }
        }
    }
}