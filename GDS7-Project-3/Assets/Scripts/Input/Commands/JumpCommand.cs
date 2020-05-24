using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class JumpCommand : Command
    {
        private Transform _transform;
        private Rigidbody _rigidbody;
        private bool _isGrounded = true;
        private Transform _groundChecker;

        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpForwardForce;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundDistance = 0.2f;

        private void Awake()
        {
            _transform = transform;
            _groundChecker = _transform.GetChild(0);
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Execute()
        {
            _isGrounded = Physics.CheckSphere(_groundChecker.position, _groundDistance, _groundLayer, QueryTriggerInteraction.Ignore);

            if (_isGrounded)
            {
                _rigidbody.AddForce(Vector3.up * Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                _rigidbody.AddForce(_transform.forward * _jumpForwardForce, ForceMode.VelocityChange);
            }
        }
    }
}