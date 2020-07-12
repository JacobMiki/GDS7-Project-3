using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GDS7.Group1.Project3.Assets.Scripts.Scene
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private GameObject _loadingScreen;
        void Start()
        {
            _loadingScreen.SetActive(true);
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
                yield return SceneManager.LoadSceneAsync(levelPart.SceneName, LoadSceneMode.Additive);
            }

            SceneManager.SetActiveScene(levelScene);
            _loadingScreen.SetActive(false);
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
                scene = SceneManager.GetSceneByPath(levelPart.ScenePath);
                if (scene.isLoaded)
                {
                    yield return SceneManager.UnloadSceneAsync(scene);
                }
            }
        }
    }
}