using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private GameObject _background;
    [SerializeField] private GameObject _menus;
    [SerializeField] private GameObject _canSkipText;
    [SerializeField] private Selectable _video;
    [SerializeField] private VideoPlayer _introPlayer;
    [SerializeField] private Level _startingLevel;

    public void StartGame()
    {
        _blackScreen.SetActive(true);
        Destroy(_background);
        Destroy(_menus);
        _video.gameObject.SetActive(true);
        _video.Select();
        _introPlayer.loopPointReached += _introPlayer_loopPointReached;
        _introPlayer.targetCamera = Camera.main;
        _introPlayer.Play();
        StartCoroutine(WaitForLevelReady());
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        StartCoroutine(LevelLoader.LoadLevel(_startingLevel));
    }

    private void _introPlayer_loopPointReached(VideoPlayer source)
    {
        SwitchToGame();
    }

    public void SkipIntro()
    {
        if (LevelLoader.LevelReady)
        {
            _introPlayer.loopPointReached -= _introPlayer_loopPointReached;
            SwitchToGame();
        }
    }

    private IEnumerator WaitForLevelReady()
    {
        while (!LevelLoader.LevelReady)
        {
            yield return null;
        }
        Application.backgroundLoadingPriority = ThreadPriority.Normal;
        _canSkipText.SetActive(true);
    }

    private void SwitchToGame()
    {
        SceneManager.activeSceneChanged += (oldS, newS) =>
        {
            SceneManager.UnloadSceneAsync("_Menu");
        };
        LevelLoader.CanActivateLevel = true;
        Destroy(_introPlayer.gameObject, 0);
    }
}
