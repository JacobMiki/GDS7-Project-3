using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Input;
using GDS7.Group1.Project3.Assets.Scripts.Input.Commands;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CharacterInput : MonoBehaviour, ILookInput, IMoveInput
    {
        [Header("Input Commands")]
        [SerializeField] private Command _move;
        [SerializeField] private Command _look;
        [SerializeField] private Command _interact;

        public Vector2 LookDelta { get; private set; }
        public Vector3 MoveDirection { get; private set; }



        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            if (_look != null)
            {
                _inputActions.Player.Look.performed += OnLook;
                _inputActions.Player.Look.canceled += OnLook;
            }
            if (_move != null)
            {
                _inputActions.Player.Move.performed += OnMove;
                _inputActions.Player.Move.canceled += OnMove;
            }

            if (_interact != null)
            {
                _inputActions.Player.Interact.performed += OnInteract;
            }
        }

        private void OnInteract(InputAction.CallbackContext obj)
        {
            _interact?.Execute();
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            LookDelta = value;
            _look?.Execute();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            MoveDirection = new Vector3(value.x, 0, value.y);
            _move?.Execute();

        }

        private void OnDisable()
        {
            if (_look != null)
            {
                _inputActions.Player.Look.performed -= OnLook;
                _inputActions.Player.Look.canceled -= OnLook;
            }
            if (_move != null)
            {
                _inputActions.Player.Move.performed -= OnMove;
                _inputActions.Player.Move.canceled -= OnMove;
            }
            if (_interact != null)
            {
                _inputActions.Player.Interact.performed -= OnInteract;
            }

            _inputActions.Disable();
        }
    }
}
