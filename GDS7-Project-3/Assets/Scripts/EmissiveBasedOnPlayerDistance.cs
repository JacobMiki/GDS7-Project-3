using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    [RequireComponent(typeof(Renderer))]
    public class EmissiveBasedOnPlayerDistance : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _emissionCurve;
        [SerializeField] private float _emissionIntensity;

        private Renderer _renderer;
        private MaterialPropertyBlock _mpb;

        private void Start()
        {
            _mpb = new MaterialPropertyBlock();
            _renderer = GetComponent<Renderer>();
            _renderer.GetPropertyBlock(_mpb);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            var distanceToPlayer = GameManager.Instance.Player
                ? Vector3.Distance(GameManager.Instance.Player.transform.position, transform.position)
                : float.MaxValue;

            _renderer.GetPropertyBlock(_mpb);

            var intensity = _emissionCurve.Evaluate(distanceToPlayer) * _emissionIntensity;
            intensity = Mathf.Clamp(intensity, 0f, 1f);
            _mpb.SetFloat("_EmissiveIntensity", intensity);

            _renderer.SetPropertyBlock(_mpb);
        }
    }
}