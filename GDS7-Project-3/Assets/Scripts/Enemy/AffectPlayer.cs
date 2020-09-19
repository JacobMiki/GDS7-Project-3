using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Input.Commands;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Enemy
{
    public class AffectPlayer : MonoBehaviour
    {
        [SerializeField] private float _inCombatSpeed;

        private MoveAndRotateForwardCommand _moveCommand;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _moveCommand = other.GetComponent<MoveAndRotateForwardCommand>();
                _moveCommand.SetSpeed(_inCombatSpeed);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _moveCommand = other.GetComponent<MoveAndRotateForwardCommand>();
                _moveCommand.ResetSpeed();
            }
        }

        private void OnDestroy()
        {
            if (_moveCommand)
            {
                _moveCommand.ResetSpeed();
            }
        }
    }
}