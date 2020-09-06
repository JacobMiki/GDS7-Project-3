using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDS7.Group1.Project3.Assets.Scripts.Scene
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private bool _loadOnStart;

        public static bool CanActivateLevel = false;
        public static bool LevelReady = false;

        void Start()
        {
            if (Application.isEditor && _loadOnStart)
            {
                StartCoroutine(StartAsync());
            } else {
                var scene = SceneManager.GetSceneByName("_Menu");
                if (!scene.isLoaded)
                {
                    SceneManager.LoadSceneAsync("_Menu", LoadSceneMode.Additive);
                }
            }
        }

        public IEnumerator StartAsync()
        {
            yield return UnloadLevel(_level);
            yield return LoadLevel(_level);
        }

        public static IEnumerator LoadLevel(Level level)
        {
            LevelReady = false;
            var levelScene = SceneManager.CreateScene(level.Name);


            var scenesToLoad = new List<AsyncOperation>();
            foreach (var levelPart in level.Parts)
            {
                var sceneLoadOperation = SceneManager.LoadSceneAsync(levelPart.SceneName, LoadSceneMode.Additive);
                sceneLoadOperation.allowSceneActivation = false;
                scenesToLoad.Add(sceneLoadOperation);
            }

            float progress = 0;
            while (progress < 0.9f)
            {
                progress = 0;
                for (int i = 0; i < scenesToLoad.Count; i++)
                {
                    progress += scenesToLoad[i].progress;
                }
                progress /= scenesToLoad.Count;
                Debug.Log($"Level loading progess: {progress}");
                yield return null;
            }

            LevelReady = true;

            while (!CanActivateLevel)
            {
                yield return null;
            }

            for (int i = 0; i < scenesToLoad.Count; i++)
            {
                scenesToLoad[i].allowSceneActivation = true;
            }

            for (int i = 0; i < scenesToLoad.Count; i++)
            {
                while (!scenesToLoad[i].isDone)
                {
                    yield return null;
                }
            }

            SceneManager.SetActiveScene(levelScene);
        }

        public static IEnumerator UnloadLevel(Level level)
        {
            var scene = SceneManager.GetSceneByName(level.Name);
            if (scene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }

            foreach (var levelPart in level.Parts)
            {
                scene = SceneManager.GetSceneByPath(levelPart.ScenePath);
                if (scene.isLoaded)
                {
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}