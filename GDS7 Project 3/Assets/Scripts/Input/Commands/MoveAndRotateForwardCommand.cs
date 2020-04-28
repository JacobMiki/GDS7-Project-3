using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class MoveAndRotateForwardCommand : Command
    {
        private Transform _transform;
        private IMoveInput _move;
        private Rigidbody _rigidbody;
        private Coroutine _moveCoroutine;

        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private AnimationCurve _speed;
        [SerializeField] private float _speedModifier = 1;
        [SerializeField] private float _rotationSmoothing = 1;

        private void Awake()
        {
            _transform = transform;
            _move = GetComponent<IMoveInput>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Execute()
        {
            if (_moveCoroutine == null)
            {
                _moveCoroutine = StartCoroutine(Move());
            }
        }

        private IEnumerator Move()
        {
            while (_move.MoveDirection != Vector3.zero)
            {
                var time = (Time.fixedDeltaTime * _speed.Evaluate(_move.MoveDirection.magnitude) * _speedModifier);

                var moveDirection = _cameraTarget.forward * _move.MoveDirection.z + _cameraTarget.right * _move.MoveDirection.x + _cameraTarget.up * _move.MoveDirection.y;

                _rigidbody.MoveRotation(Quaternion.Lerp(_transform.rotation, Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z), transform.up), _rotationSmoothing));
                _rigidbody.MovePosition(_transform.position + _transform.forward * moveDirection.magnitude * time);

                yield return null;
            }

            _moveCoroutine = null;
        }
    }
}
