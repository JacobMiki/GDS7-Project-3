using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GDS7.Group1.Project3.Assets.Scripts.Scene;
using UnityEditor;
using UnityEngine;

namespace GDS7.Group1.Project3.Assets.Editor.Scene
{
    [CustomEditor(typeof(LevelPart))]
    public class LevelPartEditor : UnityEditor.Editor
    {
        SerializedProperty SceneName;
        SerializedProperty ScenePath;

        string[] scenes;

        void OnEnable()
        {
            SceneName = serializedObject.FindProperty("SceneName");
            ScenePath = serializedObject.FindProperty("ScenePath");
            scenes = AssetDatabase.FindAssets("t:Scene", null).Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();

        }
        public override void OnInspectorGUI()
        {
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(ScenePath.stringValue);

            serializedObject.Update();

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUILayout.ObjectField("Scene", oldScene, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                ScenePath.stringValue = newPath;
                SceneName.stringValue = newPath.Replace("Assets/", "").Replace("Packages/", "").Replace(".unity", "");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}