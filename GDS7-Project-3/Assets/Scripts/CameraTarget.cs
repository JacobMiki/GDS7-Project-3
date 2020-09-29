using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup;

        public void AddPlayer()
        {
            var player = GameManager.Instance.Player;
            player.GetComponent<CharacterInput>().CameraEnabled = false;
            _cinemachineTargetGroup.AddMember(player.transform, 1f, 0f);
        }

        public void RemovePlayer()
        {
            var player = GameManager.Instance.Player;
            player.GetComponent<CharacterInput>().CameraEnabled = true;
            _cinemachineTargetGroup.RemoveMember(player.transform);
        }

        public void PlayThinkingSound()
        {
            var player = GameManager.Instance.Player;
            player.GetComponent<PlayerSounds>().Play(PlayerSoundTypes.THINKING);
        }
    }
}