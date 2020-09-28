using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class CameraLookCommand : Command
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Vector2 _sensitivity = Vector2.one;

        private ILookInput _look;
        private CinemachineFreeLook _freeLookCamera;
        private Coroutine _lookCoroutine;
        private WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();


        private void Awake()
        {
            _look = GetComponent<ILookInput>();
            _freeLookCamera = _camera.GetComponent<CinemachineFreeLook>();
            _freeLookCamera.m_XAxis.m_InputAxisName = "";
            _freeLookCamera.m_YAxis.m_InputAxisName = "";
        }

        public override void Execute()
        {
            if (_lookCoroutine == null)
            {
                _lookCoroutine = StartCoroutine(Look());
            }
        }

        private IEnumerator Look()
        {
            while (_look.LookDelta != Vector2.zero)
            {
                yield return _waitForFixedUpdate;

                _freeLookCamera.m_XAxis.m_InputAxisValue = _look.LookDelta.x * _sensitivity.x * GameManager.Instance.Settings.horizontalSensitivity;
                _freeLookCamera.m_YAxis.m_InputAxisValue = _look.LookDelta.y * _sensitivity.y * GameManager.Instance.Settings.verticalSensitivity;

            }

            _freeLookCamera.m_XAxis.m_InputAxisValue = 0;
            _freeLookCamera.m_YAxis.m_InputAxisValue = 0;

            _lookCoroutine = null;
        }
    }
}
