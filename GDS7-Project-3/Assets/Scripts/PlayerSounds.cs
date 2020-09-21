using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private PlayerSoundSet _playerSoundSet;
        [SerializeField] private float _pitchVariation;

        [Header("Breathing")]
        [SerializeField] private float _breathingTimeIntervalMin;
        [SerializeField] private float _breathingTimeIntervalMax;

        private Dictionary<PlayerSoundTypes, PlayerSound> _sounds;
        private float _startingPitch;
        private float _startingVolume;

        private void Awake()
        {
            _sounds = _playerSoundSet.playerSounds.ToDictionary(ps => ps.type, ps => ps);
            _startingPitch = _audioSource.pitch;
            _startingVolume = _audioSource.volume;
        }

        private void OnEnable()
        {
            StartCoroutine(Breathe());
        }

        public void Play(PlayerSoundTypes type)
        {
            if (_sounds.TryGetValue(type, out var ps) && ps.clips.Length > 0)
            {
                var clip = ps.clips[Random.Range(0, ps.clips.Length)];

                _audioSource.pitch = Random.Range(_startingPitch - _pitchVariation, _startingPitch + _pitchVariation);
                _audioSource.volume = _startingVolume * ps.volume;
                _audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning("Missing soundclips for type " + type.ToString(), this);
            }
        }

        private IEnumerator Breathe()
        {
            while(isActiveAndEnabled)
            {
                yield return new WaitForSeconds(Random.Range(_breathingTimeIntervalMin, _breathingTimeIntervalMax));
                Play(PlayerSoundTypes.BREATHE);
            }
        }
        
    }
}