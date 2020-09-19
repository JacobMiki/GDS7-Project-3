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
        public Quaternion LastSafeRotation { get; set; }
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

        private void LateUpdate()
        {
            var dist = 1f;

            foreach (Transform checker in _groundChecker.transform)
            {
                if (Physics.Raycast(checker.position, Vector3.down, out var hit, 1f, _groundLayer, QueryTriggerInteraction.Ignore))
                {
                    if (dist > hit.distance)
                    {
                        dist = hit.distance;
                    }
                }
            }
            DistanceFromGround = dist;
            IsGrounded = DistanceFromGround <= _groundDistance;
        }

        public GameObject DropTorch()
        {
            var torch = Instantiate(_torch, _torch.transform.parent);
            var rb = torch.AddComponent<Rigidbody>();
            rb.mass = 1f;
            rb.isKinematic = false;
            rb.useGravity = true;
            var collider = torch.AddComponent<CapsuleCollider>();
            collider.radius = 0.1f;
            collider.center = new Vector3(0.1f, 0, 0);
            torch.layer = LayerMask.NameToLayer("Default");
            HasTorch = false;
            return torch;
        }
    }
}
