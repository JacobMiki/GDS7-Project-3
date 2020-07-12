using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Scene
{
    [CreateAssetMenu(fileName = "Level", menuName = "Data/Level", order = 1)]
    public class Level : ScriptableObject
    {
        public string Name;
        public LevelPart[] Parts;
    }
}