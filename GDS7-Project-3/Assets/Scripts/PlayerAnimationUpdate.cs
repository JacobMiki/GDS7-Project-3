using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Input;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class PlayerAnimationUpdate : MonoBehaviour
    {
        private IGroundedState _groundedState;
        private IMoveInput _moveInput;
        private ITorchState _torchState;

        [SerializeField] private Animator _animator;
        [SerializeField] private float _runningThreshold;


        // Start is called before the first frame update
        void Start()
        {
            _groundedState = GetComponent<IGroundedState>();
            _moveInput = GetComponent<IMoveInput>();
            _torchState = GetComponent<ITorchState>();
        }

        // Update is called once per frame
        void Update()
        {
            _animator.SetBool("HasTorch", _torchState.HasTorch);
            _animator.SetBool("IsGrounded", _groundedState.IsGrounded);
            _animator.SetBool("IsWalking", _moveInput.MoveDirection.magnitude > 0 && _moveInput.MoveDirection.magnitude < _runningThreshold);
            _animator.SetBool("IsRunning", _moveInput.MoveDirection.magnitude >= _runningThreshold);
        }
    }
}