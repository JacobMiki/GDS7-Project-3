using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Triggers
{
    public class TriggerWithCooldown : Trigger
    {
        [SerializeField] private float _encounterCooldown;

        private WaitForSeconds _waitForEncounterCooldown;
        private Coroutine _cooldownCoroutine;

        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
            _waitForEncounterCooldown = new WaitForSeconds(_encounterCooldown);
        }

        protected override bool HandleTrigger(Collider other)
        {
            _enabled = false;
            return true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (_cooldownCoroutine == null)
                {
                    _cooldownCoroutine = StartCoroutine(Cooldown());
                }
            }
        }

        private IEnumerator Cooldown()
        {
            yield return _waitForEncounterCooldown;
            _enabled = true;
            _cooldownCoroutine = null;
        }
    }
}