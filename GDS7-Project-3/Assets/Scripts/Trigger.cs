using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool _oneShot;
    [SerializeField] private UnityEvent _onTrigger = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _onTrigger.Invoke();
            if (_oneShot)
            {
                Destroy(gameObject);
            }
        }
    }
}
