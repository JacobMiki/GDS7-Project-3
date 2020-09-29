using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    public class CollectableInteract : MonoBehaviour
    {
        [SerializeField] private float _animationTime;
        [SerializeField] private float _timeToPickUp = 1f;
        [SerializeField] private Transform _animationSnapPoint;
        [SerializeField] private GameObject _model;
        [SerializeField] private TextMeshPro _hintText;

        public bool collected = false;

        private void OnEnable()
        {
            CollectableManager.Instance.collectableInteracts.Add(this);
        }

        public void Interact(GameObject interacting, InteractionZone zone)
        {
            if(collected)
            {
                return;
            }

            StartCoroutine(PickUp());
            var animator = interacting.GetComponentInChildren<Animator>();
            if (animator)
            {
                interacting.GetComponent<Rigidbody>().velocity = Vector3.zero;
                interacting.transform.position = _animationSnapPoint.position;
                interacting.transform.rotation = _animationSnapPoint.rotation;
                animator.SetTrigger("Pick Up");
            }
            StartCoroutine(WaitToEndInteraction(zone));
        }

        IEnumerator WaitToEndInteraction(InteractionZone zone)
        {
            yield return new WaitForSeconds(_animationTime);
            zone.IsInteracting = false;
        }

        IEnumerator PickUp()
        {
            yield return new WaitForSeconds(_timeToPickUp);
            Destroy(_model);
            collected = true;
            CollectableManager.Instance.UpdateCollectables();
            _hintText.text = $"{CollectableManager.Instance.collectedCount} of {CollectableManager.Instance.collectableInteracts.Count}";
        }
    }
}