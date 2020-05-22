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

        private ILookInput _look;
        private CinemachineFreeLook _freeLookCamera;
        private Coroutine _lookCoroutine;


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

                _freeLookCamera.m_XAxis.m_InputAxisValue = _look.LookDelta.x;
                _freeLookCamera.m_YAxis.m_InputAxisValue = _look.LookDelta.y;

                yield return null;
            }

            _freeLookCamera.m_XAxis.m_InputAxisValue = 0;
            _freeLookCamera.m_YAxis.m_InputAxisValue = 0;

            _lookCoroutine = null;
        }
    }
}
