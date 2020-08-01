using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup;

    public void AddPlayer()
    {
        _cinemachineTargetGroup.AddMember(GameObject.FindGameObjectWithTag("Player").transform, 1f, 0f);
    }

    public void RemovePlayer()
    {
        _cinemachineTargetGroup.RemoveMember(GameObject.FindGameObjectWithTag("Player").transform);
    }
}
