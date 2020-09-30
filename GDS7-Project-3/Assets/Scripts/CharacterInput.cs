using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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

        private bool _paused = false;
        private bool _onUnpauseCameraEnabled = false;
        private bool _onUnpauseInputsEnabled = false;
        private void OnPause(InputAction.CallbackContext obj)
        {
            _paused = !_paused;

            var screens = GameObject.FindGameObjectWithTag("Screens");
            var pause = screens.transform.Find("Pause");
            pause.gameObject.SetActive(_paused);

            Cursor.visible = _paused;
            if (_paused)
            {
                _onUnpauseCameraEnabled = CameraEnabled;
                _onUnpauseInputsEnabled = InputsEnabled;
                CameraEnabled = false;
                InputsEnabled = false;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
                StartCoroutine(WaitForPauseEnd(pause.gameObject));
            }
            else
            {
                CameraEnabled = _onUnpauseCameraEnabled;
                InputsEnabled = _onUnpauseInputsEnabled;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
        }

        private IEnumerator WaitForPauseEnd(GameObject pause)
        {
            while(pause.activeSelf)
            {
                yield return null;
            }

            _paused = false;
            Cursor.visible = false;
            CameraEnabled = _onUnpauseCameraEnabled;
            InputsEnabled = _onUnpauseInputsEnabled;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
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
