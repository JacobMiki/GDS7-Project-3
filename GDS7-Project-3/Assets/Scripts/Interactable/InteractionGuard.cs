using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using UnityEngine;

[RequireComponent(typeof(InteractionZone))]
public abstract class InteractionGuard : MonoBehaviour
{
    public abstract bool CanInteract(GameObject interacting, InteractionZone zone = null);
}
