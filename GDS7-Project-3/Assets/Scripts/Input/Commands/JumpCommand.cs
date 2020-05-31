using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class JumpCommand : Command
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private IGroundedState _groundedState;

        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpForwardForce;

        private void Awake()
        {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
            _groundedState = GetComponent<IGroundedState>();
        }

        public override void Execute()
        {

            if (_groundedState.IsGrounded)
            {
                _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                _rigidbody.AddForce(_transform.forward * _jumpForwardForce, ForceMode.VelocityChange);
            }
        }
    }
}