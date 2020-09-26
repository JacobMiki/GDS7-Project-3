using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;

public class TorchInteraction : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _swingSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Collider _interactionTriggerCollider;

    public bool isSwinging = false;

    private void Start()
    {
        _interactionTriggerCollider.enabled = false;
    }

    public void StartSwing()
    {
        isSwinging = true;
        _interactionTriggerCollider.enabled = true;
        _animator.SetTrigger("Attack");
        _audioSource.PlayOneShot(_swingSound);
    }

    public void EndSwing()
    {
        isSwinging = false;
        _interactionTriggerCollider.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isSwinging)
        {
            return;
        }

        var interactable = other.GetComponent<IInteractable>();

        interactable?.Interact();
        isSwinging = false;
    }
}
