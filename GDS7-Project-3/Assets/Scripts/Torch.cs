using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class Torch : MonoBehaviour
    {
        [SerializeField] private Light _light;
        [SerializeField] private VisualEffect _vfx;

        private readonly string _vfxColorName = "Color Over Life";
        private readonly string _smokinessName = "Smokiness";

        private float _lightIntensity;
        private Gradient _vfxColor;
        void Start()
        {
            _lightIntensity = _light.intensity;
            _vfxColor = _vfx.GetGradient(_vfxColorName);
        }

        public void ChangeColor(Color color, float intensity)
        {
            _light.intensity = intensity;
            _light.color = color;
            var grad = new Gradient();
            grad.SetKeys(
                new GradientColorKey[] { 
                    new GradientColorKey(color, 0f),
                    new GradientColorKey(color, 1f) 
                },
                _vfxColor.alphaKeys
            );
            _vfx.SetGradient(_vfxColorName, grad);
            _vfx.SetFloat(_smokinessName, intensity / _lightIntensity);

        }

        public void ResetColor()
        {
            _light.intensity = _lightIntensity;
            _light.color = Color.white;
            _vfx.SetGradient(_vfxColorName, _vfxColor);
            _vfx.SetFloat(_smokinessName, 1f);
        }


    }
}