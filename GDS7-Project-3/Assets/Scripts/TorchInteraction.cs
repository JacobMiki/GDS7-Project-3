using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;

public class TorchInteraction : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip _swingSound;
    [SerializeField] private AudioSource _audioSource;

    private bool _isSwinging = false;

    public void StartSwing()
    {
        _isSwinging = true;
        _animator.SetTrigger("Attack");
        _audioSource.PlayOneShot(_swingSound);
    }

    public void EndSwing()
    {
        _isSwinging = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isSwinging)
        {
            return;
        }

        var interactable = other.GetComponent<IInteractable>();
        Debug.Log(interactable, this);

        interactable?.Interact();
        _isSwinging = false;
    }
}
