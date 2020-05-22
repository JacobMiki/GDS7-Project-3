using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    public class SwitchableLight : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject _light;

        public void Awake()
        {
            _light.SetActive(false);
        }
        public void Interact()
        {
            _light.SetActive(true);
        }

    }
}
