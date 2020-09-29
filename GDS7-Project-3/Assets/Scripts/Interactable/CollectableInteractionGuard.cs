using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Interactable
{
    [RequireComponent(typeof(CollectableInteract))]
    public class CollectableInteractionGuard : InteractionGuard
    {
        private CollectableInteract _collectable;

        void OnEnable()
        {
            _collectable = GetComponent<CollectableInteract>();
        }

        public override bool CanInteract(GameObject interacting, InteractionZone zone = null)
        {
            return !_collectable.collected;
        }
    }
}