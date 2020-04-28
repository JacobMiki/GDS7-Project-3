using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Input.Commands
{
    class CameraLookCommand : Command
    {
        [SerializeField] private Transform _cameraTarget;
        [SerializeField] private float _horizontalSensitivity;
        [SerializeField] private float _verticalSensitivity;

        private ILookInput _look;
        private Coroutine _lookCoroutine;


        private void Awake()
        {
            _look = GetComponent<ILookInput>();
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
                var newX = _cameraTarget.eulerAngles.x + _look.LookDelta.y * Time.fixedDeltaTime * _verticalSensitivity;
                var newY = _cameraTarget.eulerAngles.y + _look.LookDelta.x * Time.fixedDeltaTime * _horizontalSensitivity;

                _cameraTarget.rotation = Quaternion.Euler(newX, newY, 0);

                yield return null;
            }

            _lookCoroutine = null;
        }
    }
}
