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

        [SerializeField] private Animator _animator;


        // Start is called before the first frame update
        void Start()
        {
            _groundedState = GetComponent<IGroundedState>();
            _moveInput = GetComponent<IMoveInput>();
        }

        // Update is called once per frame
        void Update()
        {
            _animator.SetBool("IsGrounded", _groundedState.IsGrounded);
            _animator.SetFloat("WalkSpeed", _moveInput.MoveDirection.magnitude);
        }
    }
}