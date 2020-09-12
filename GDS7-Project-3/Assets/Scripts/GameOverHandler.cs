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
            yield return new WaitForSeconds(_gameOverTimeout);
            transform.position = GetComponent<ISafeState>().LastSafePosition;
            transform.rotation = GetComponent<ISafeState>().LastSafeRotation;
            GetComponent<ISafeState>().IsSafe = true;
            GetComponent<ITorchState>().HasTorch = true;
        }
    }
}
