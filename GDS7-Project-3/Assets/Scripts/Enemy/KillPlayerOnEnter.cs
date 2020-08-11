using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets
{
    public class KillPlayerOnEnter : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var torchState = other.GetComponent<ITorchState>();
                torchState.HasTorch = false;
                Destroy(_enemy, 1f);

                var gameOverHandler = other.GetComponent<GameOverHandler>();
                if (gameOverHandler)
                {
                    gameOverHandler.GameOver();
                }
            }
        }
    }
}