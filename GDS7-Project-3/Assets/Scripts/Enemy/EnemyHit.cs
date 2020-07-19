using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets
{
    public class EnemyHit : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private int _hitsToKill;
        
        public void Interact()
        {
            _hitsToKill--;
            if (_hitsToKill <= 0)
            {
                Destroy(_enemy);
            }
        }
    }
}