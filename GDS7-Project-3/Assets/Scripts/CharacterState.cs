﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CharacterState : MonoBehaviour, IGroundedState, ISafeState
    {
        public bool IsGrounded { get; private set; }
        public bool IsSafe { get; set; }

        [Header("Grounded state")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundDistance = 0.1f;
        [SerializeField] private Animator _animator;

        private Transform _transform;
        private Transform _groundChecker;

        private void Awake()
        {
            _transform = transform;
            _groundChecker = _transform.GetChild(0);
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics.CheckSphere(_groundChecker.position, _groundDistance, _groundLayer, QueryTriggerInteraction.Ignore);
        }

        public void AddSwing()
        {
            var name = "Torch_SwingsInLast2s";
            _animator.SetInteger(name, _animator.GetInteger(name) + 1);
            StartCoroutine(RemoveSwingIn(2));
        }

        private IEnumerator RemoveSwingIn(float s)
        {
            yield return new WaitForSeconds(s);
            var name = "Torch_SwingsInLast2s";
            _animator.SetInteger(name, _animator.GetInteger(name) - 1);

        }
    }
}
