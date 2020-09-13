using System;
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
        [SerializeField] private Command _jump;

        public Vector2 LookDelta { get; set; }
        public Vector3 MoveDirection { get; set; }

        public bool InputsEnabled { get; set; }
        public bool CameraEnabled { get; set; }

        private PlayerInputActions _inputActions;

        private void Awake()
        {

        }

        private void OnEnable()
        {
            _inputActions = new PlayerInputActions();
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            InputsEnabled = true;
            CameraEnabled = true;

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

            if (_jump != null)
            {
                _inputActions.Player.Jump.performed += OnJump;
            }

            _inputActions.Player.Pause.performed += OnPause;

        }

        private void OnPause(InputAction.CallbackContext obj)
        {
#if DEBUG
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
#else
            Application.Quit();
#endif
        }

        private void OnInteract(InputAction.CallbackContext obj)
        {
            if (_interact != null && InputsEnabled)
            {
                _interact.Execute();
            }
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (_jump != null && InputsEnabled)
            {
                _jump.Execute();
            }
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            if (Cursor.lockState == CursorLockMode.Locked && CameraEnabled)
            {
                var value = context.ReadValue<Vector2>();
                LookDelta = value;
                if (_look != null)
                {
                    _look.Execute();
                }
            }
        }

        private void OnMove(InputAction.CallbackContext context)
        {   
            if (InputsEnabled)
            {
                var value = context.ReadValue<Vector2>();
                MoveDirection = new Vector3(value.x, 0, value.y).normalized;
                if (_move != null)
                {
                    _move.Execute();
                }
            }
            else
            {
                MoveDirection = new Vector3(0, 0, 0);
            }


        }

        private void OnDestroy()
        {
            OnDisable();
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
            if (_jump != null)
            {
                _inputActions.Player.Jump.performed -= OnJump;
            }
            _inputActions.Player.Pause.performed -= OnPause;

            _inputActions.Disable();
        }
    }
}
