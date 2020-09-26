using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CatLookAtPlayer : MonoBehaviour
    {
        [SerializeField] private Transform _head;

        private GameObject _player;
        private Quaternion _rot;
        private bool _looking = true;

        public void Looking(bool l)
        {
            _looking = l;
        }

        void OnEnable()
        {
            _player = GameObject.Find("Player");
        }

        void LateUpdate()
        {
            if (_player && _head)
            {
                var target = _head.rotation;
                if (_looking)
                {
                    target = Quaternion.LookRotation(_player.transform.position - _head.position);
                }
                _rot = Quaternion.Slerp(_rot, target, Time.deltaTime);
                _head.rotation = _rot;
            }
        }
    }
}