using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Interactable;
using GDS7.Group1.Project3.Assets.Scripts.State;
using UnityEngine;

[RequireComponent(typeof(SwitchableLight))]
public class SwitchableLightInteractionGuard : InteractionGuard
{
    private SwitchableLight _light;

    void OnEnable()
    {
        _light = GetComponent<SwitchableLight>();
    }

    public override bool CanInteract(GameObject interacting, InteractionZone zone = null)
    {
        var torchState = interacting.GetComponent<ITorchState>();
        if (torchState == null || !torchState.HasTorch)
        {
            return false;
        }
        return !_light.IsLightOn;
    }
}
