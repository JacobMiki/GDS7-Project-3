using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CatLookAtPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _head;
        [SerializeField][Range(0, 180)] private float _maxRotation = 70;

        private GameObject _player;
        private Quaternion _rot;
        private Quaternion _target;
        private bool _looking = true;

        public void Looking(bool l)
        {
            _looking = l;
        }

        void OnEnable()
        {
            _player = GameManager.Instance.Player;
        }

        void LateUpdate()
        {
            if (_player && _head)
            {
                var target = _head.rotation;

                if (_looking)
                {
                    var targetProp = Quaternion.LookRotation(_player.transform.position + Vector3.up * 0.4f - _head.position);
                    var angle = Quaternion.Angle(_head.rotation, targetProp);
                    if (angle < _maxRotation)
                    {
                        _target = targetProp;
                    }
                }
                else
                {
                    _target = _head.rotation;
                }

                _rot = Quaternion.Slerp(_rot, _target, Time.deltaTime);
                _head.rotation = _rot;
            }
        }
    }
}