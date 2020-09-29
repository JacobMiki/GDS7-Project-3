using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    [RequireComponent(typeof(Light))]
    public class TurnOffOnStart : MonoBehaviour
    {
        private Light _light;

        void Start()
        {
            _light = GetComponent<Light>();
            _light.intensity = 0.0f;
        }
    }
}