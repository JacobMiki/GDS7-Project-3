﻿using System.Collections;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class JumpCommand : Command
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private IGroundedState _groundedState;
        private IMoveInput _moveInput;
        private PlayerSounds _sounds;

        [SerializeField] private Animator _animator;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpForwardForce;
        [SerializeField] private float _takeoffTime;

        private bool _jumpDisabled = false;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _groundedState = GetComponent<IGroundedState>();
            _moveInput = GetComponent<IMoveInput>();
            _sounds = GetComponent<PlayerSounds>();
        }

        public override void Execute()
        {

            if (!_jumpDisabled && _groundedState.IsGrounded)
            {
                _animator.SetTrigger("Jump");
                _sounds.Play(PlayerSoundTypes.JUMP);
                _jumpDisabled = true;
                StartCoroutine(JumpAfterTakeoff());
            }
        }

        public IEnumerator JumpAfterTakeoff()
        {
            yield return new WaitForSeconds(_takeoffTime);
            _jumpDisabled = false;
            // _rigidbody.AddForce(_moveInput.MoveDirection.magnitude * _transform.forward * _jumpForwardForce, ForceMode.VelocityChange);
            _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
    }
}