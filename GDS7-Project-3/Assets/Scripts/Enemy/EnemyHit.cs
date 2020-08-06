using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Enemy
{
    public class EnemyHit : MonoBehaviour, IInteractable
    {
        [SerializeField] private EnemyController _enemy;
        [SerializeField] private int _hitsToKill;

        public void Interact()
        {
            _hitsToKill--;
            Debug.Log("Enemy hit! Hits left: " + _hitsToKill.ToString(), this);
            if (_hitsToKill <= 0)
            {
                _enemy.Defeat();
            }
        }
    }
}