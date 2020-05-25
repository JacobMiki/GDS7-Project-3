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

        [SerializeField] private Transform _camera;
        [SerializeField] private float _speed;
        [SerializeField] private float _turnSmoothTime;

        private float _turnSmoothVelocity;

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
                var targetAngle = Mathf.Atan2(_move.MoveDirection.x, _move.MoveDirection.z) * Mathf.Rad2Deg + _camera.eulerAngles.y;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _turnSmoothTime);

                var moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                _rigidbody.MoveRotation(Quaternion.Euler(_transform.rotation.eulerAngles.x, angle, _transform.rotation.eulerAngles.z));
                _rigidbody.MovePosition(_transform.position + moveDirection.normalized * _speed * Time.deltaTime);


                yield return null;
            }

            _moveCoroutine = null;
        }
    }
}
