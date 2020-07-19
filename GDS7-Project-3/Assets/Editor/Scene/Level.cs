using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Scene;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Editor.Scene
{
    [CustomEditor(typeof(Level))]
    public class LevelEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var level = (Level)target;

            if (GUILayout.Button("Load level"))
            {
                for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
                {
                    var scene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(i);
                    if (!scene.name.StartsWith("_"))
                    {
                        EditorSceneManager.CloseScene(scene, true);
                    }
                }

                foreach (var levelPart in level.Parts)
                {
                    EditorSceneManager.OpenScene(levelPart.ScenePath, OpenSceneMode.Additive);
                }
            }
        }
    }
}