using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Triggers
{
    public class TriggerWithCooldown : Trigger
    {
        [SerializeField] private float _encounterCooldown;

        private WaitForSeconds _waitForEncounterCooldown;

        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
            _waitForEncounterCooldown = new WaitForSeconds(_encounterCooldown);
        }

        protected override bool HandleTrigger(Collider other)
        {
            StartCoroutine(Cooldown());
            return true;
        }

        private IEnumerator Cooldown()
        {
            _enabled = false;
            yield return _waitForEncounterCooldown;
            _enabled = true;
        }
    }
}