using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class Footsteps : MonoBehaviour
    {
        [SerializeField] private GameObject _rightFoot;
        [SerializeField] private GameObject _leftFoot;

        [SerializeField] private AudioSource _rightFootstepAudioSource;
        [SerializeField] private AudioSource _leftFootstepAudioSource;

        [SerializeField] private GameObject _rightFootprint;
        [SerializeField] private GameObject _leftFootprint;

        [SerializeField] private AudioClip[] _footsteps;

        [SerializeField] private int _maxFootprints;
        [SerializeField] private float _offGroundDistance;

        private bool _rightFootOffGround;
        private bool _leftFootOffGround;

        private GameObject[] _footprintPool;
        private int _footprintIndex;

        private void Start()
        {
            _footprintPool = new GameObject[_maxFootprints];
            _footprintIndex = 0;
            _rightFootOffGround = false;
            _leftFootOffGround = false;
        }

        private void LateUpdate()
        {
            bool _rightFootOnGround = Physics.Raycast(new Ray(_rightFoot.transform.position, Vector3.down), _offGroundDistance, LayerMask.GetMask("World"));
            bool _leftFootOnGround = Physics.Raycast(new Ray(_leftFoot.transform.position, Vector3.down), _offGroundDistance, LayerMask.GetMask("World"));

            if (_rightFootOffGround)
            {
                if (_rightFootOnGround)
                {
                    _rightFootOffGround = false;
                    RightFootstep();
                }
            }
            else
            {
                if (!_rightFootOnGround)
                {
                    _rightFootOffGround = true;
                }
            }

            if (_leftFootOffGround)
            {
                if (_leftFootOnGround)
                {
                    _leftFootOffGround = false;
                    LeftFootstep();
                }
            }
            else
            {
                if (!_leftFootOnGround)
                {
                    _leftFootOffGround = true;
                }
            }
        }

        public void LeftFootstep()
        {
            Footstep(_leftFoot, _leftFootstepAudioSource, _leftFootprint);
        }

        public void RightFootstep()
        {
            Footstep(_rightFoot, _rightFootstepAudioSource, _rightFootprint, _maxFootprints / 2);
        }

        private void Footstep(GameObject foot, AudioSource audioSource, GameObject footprint, int skipIndex = 0)
        {
            if (_footsteps != null && _footsteps.Length > 0)
            {
                var clip = _footsteps[Random.Range(0, _footsteps.Length)];
                audioSource.PlayOneShot(clip);
            }

            if (Physics.Raycast(foot.transform.position, Vector3.down, out var hit, _offGroundDistance, LayerMask.GetMask("World"), QueryTriggerInteraction.Ignore))
            {
                Debug.DrawRay(foot.transform.position, Vector3.down);
                var footprintInstance = _footprintPool[_footprintIndex + skipIndex];
                if (footprintInstance == null)
                {
                    footprintInstance = Instantiate(footprint, hit.point, transform.rotation);
                    footprintInstance.transform.SetParent(hit.transform);
                    _footprintPool[_footprintIndex + skipIndex] = footprintInstance;
                } 
                else
                {
                    footprintInstance.transform.SetParent(hit.transform);
                    footprintInstance.transform.position = hit.point;
                    footprintInstance.transform.rotation = transform.rotation;
                }

                _footprintIndex = (_footprintIndex + 1) % (_maxFootprints / 2);
            }
        }
    }
}