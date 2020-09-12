using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private bool _closedOnStart;

        private void Start()
        {
            _animator.SetBool("ClosedOnStart", _closedOnStart);
        }

        public void Open()
        {
            _animator.SetTrigger("Open");
            _audioSource.Play();
        }

        public void Close()
        {
            _animator.SetTrigger("Close");
            _audioSource.Play();
        }
    }
}