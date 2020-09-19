using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Enemy
{
    public class AffectTorch : MonoBehaviour
    {
        [SerializeField] private Color _torchColor;
        [SerializeField] private float _torchIntensity;

        private Torch _torch;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var torch = other.GetComponentInChildren<Torch>();
                _torch = torch;
                torch.ChangeColor(_torchColor, _torchIntensity);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var torch = other.GetComponentInChildren<Torch>();
                _torch = torch;
                torch.ResetColor();
            }
        }

        private void OnDestroy()
        {
            if (_torch)
            {
                _torch.ResetColor();
            }
        }
    }
}