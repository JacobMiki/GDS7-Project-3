using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDS7.Group1.Project3.Assets.Scripts.Scene
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Level _level;
        void Start()
        {
            StartCoroutine(StartAsync());
        }

        public IEnumerator StartAsync()
        {
            if (Application.isEditor)
            {
                yield return UnloadLevel(_level);
            }
            yield return LoadLevel(_level);
        }

        public IEnumerator LoadLevel(Level level)
        {          
            var levelScene = SceneManager.CreateScene(level.Name);

            foreach (var levelPart in level.Parts)
            {
                var scene = SceneManager.GetSceneByName(levelPart.SceneName);
                if (scene.isLoaded)
                {
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
                yield return SceneManager.LoadSceneAsync(levelPart.SceneName, LoadSceneMode.Additive);
            }

            SceneManager.SetActiveScene(levelScene);
        }

        public IEnumerator UnloadLevel(Level level)
        {
            var scene = SceneManager.GetSceneByName(level.Name);
            if (scene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }

            foreach (var levelPart in level.Parts)
            {
                scene = SceneManager.GetSceneByName(levelPart.SceneName);
                if (scene.isLoaded)
                {
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}