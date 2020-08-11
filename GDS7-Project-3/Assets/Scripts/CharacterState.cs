using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CharacterState : MonoBehaviour, IGroundedState, ISafeState, ITorchState
    {
        public bool IsGrounded { get; private set; }
        public float DistanceFromGround { get; private set; }
        public bool IsSafe { get; set; }
        public Vector3 LastSafePosition { get; set; }
        public bool HasTorch { get { return _torch.activeSelf; } set { _torch.SetActive(value); } }

        [Header("Grounded state")]
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _groundDistance = 0.1f;

        [Header("Torch state")]
        [SerializeField] private GameObject _torch;
        [SerializeField] private bool _hasTorchOnStart;

        private Transform _transform;
        private Transform _groundChecker;

        private void Awake()
        {
            _transform = transform;
            _groundChecker = _transform.GetChild(0);
            HasTorch = _hasTorchOnStart;
        }

        private void FixedUpdate()
        {
            if (Physics.SphereCast(_groundChecker.position, 0.1f, Vector3.down, out var hit, 1f, _groundLayer, QueryTriggerInteraction.Ignore))
            {
                DistanceFromGround = hit.distance;
                IsGrounded = DistanceFromGround <= _groundDistance;
            }
            else
            {
                IsGrounded = false;
            }
        }
    }
}
