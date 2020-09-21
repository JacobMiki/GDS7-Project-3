using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    class GameOverHandler : MonoBehaviour
    {
        [SerializeField] private float _gameOverTimeout;
        [SerializeField] private float _killPlaneY;
        [SerializeField] private Animator _animator;
        private PlayerSounds _sounds;

        private void Awake()
        {
            _sounds = GetComponent<PlayerSounds>();
        }

        private void Update()
        {
            if (transform.position.y < _killPlaneY)
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            StartCoroutine(RunGameOver());
        }

        public IEnumerator RunGameOver()
        {
            _sounds.Play(PlayerSoundTypes.DEATH);
            yield return new WaitForSeconds(_gameOverTimeout);
            transform.position = GetComponent<ISafeState>().LastSafePosition;
            transform.rotation = GetComponent<ISafeState>().LastSafeRotation;
            GetComponent<ISafeState>().IsSafe = true;
            GetComponent<ITorchState>().HasTorch = true;
            _animator.SetTrigger("StandUp");
        }
    }
}
