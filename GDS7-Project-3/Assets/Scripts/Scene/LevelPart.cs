using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Scripts.Scene
{
    [CreateAssetMenu(fileName = "LevelPart", menuName = "Data/Level Part", order = 2)]
    public class LevelPart : ScriptableObject
    {
        public string SceneName;
        public string ScenePath;
    }
}