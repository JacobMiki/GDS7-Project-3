using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public enum PlayerSoundTypes
    {
        JUMP,
        SWING,
        CLIMB,
        DEATH,
        STAND_UP,
        BREATHE,
        THINKING,
    }

    [Serializable]
    public class PlayerSound
    {
        public PlayerSoundTypes type;
        public AudioClip[] clips;
        public float volume = 1f;
    }

    [CreateAssetMenu(fileName = "Player Sound Set", menuName = "Data/Player Sound Set")]
    public class PlayerSoundSet : ScriptableObject
    {
        public PlayerSound[] playerSounds;
    }
}