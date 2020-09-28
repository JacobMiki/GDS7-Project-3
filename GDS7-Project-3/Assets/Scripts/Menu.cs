using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Selectable _firstSelected;

        [Header("Video")]
        [SerializeField] private VideoPlayer _introPlayer;
        [SerializeField] private GameObject _canSkipText;

        [Header("Level")]
        [SerializeField] private Level _startingLevel;

        private PlayerInputActions _inputActions;


        private void OnEnable()
        {
            _inputActions = new PlayerInputActions();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (_firstSelected)
            {
                _firstSelected.Select();
            }
            _introPlayer.Prepare();
            _introPlayer.Play();
            _introPlayer.Pause();
            _introPlayer.prepareCompleted += _introPlayer_prepareCompleted;
        }

        private void _introPlayer_prepareCompleted(VideoPlayer source)
        {
            _introPlayer.prepareCompleted -= _introPlayer_prepareCompleted;
            _animator.SetTrigger("Intro");
            if (_firstSelected)
            {
                _firstSelected.Select();
            }
        }

        public void StartGame()
        {
            _animator.SetTrigger("Outro");
            _introPlayer.loopPointReached += _introPlayer_loopPointReached;
            _introPlayer.targetCamera = Camera.main;
            _introPlayer.Play();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _inputActions.Player.Interact.performed += Interact_performed;
            _inputActions.Enable();
            StartCoroutine(WaitForLevelReady());
            Application.backgroundLoadingPriority = ThreadPriority.Low;
            StartCoroutine(LevelLoader.LoadLevel(_startingLevel));
        }

        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            SkipIntro();
        }

        private void _introPlayer_loopPointReached(VideoPlayer source)
        {
            SwitchToGame();
        }

        private bool _skippingIntro = false;
        public void SkipIntro()
        {
            if (_skippingIntro)
            {
                return;
            }
            _skippingIntro = true;
            if (LevelLoader.LevelReady)
            {
                _introPlayer.loopPointReached -= _introPlayer_loopPointReached;
                _inputActions.Disable();
                _inputActions.Player.Interact.performed -= Interact_performed;
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

        private bool _switchingToGame = false;
        private void SwitchToGame()
        {
            if (_switchingToGame)
            {
                return;
            }
            _switchingToGame = true;
            SceneManager.activeSceneChanged += (oldS, newS) =>
            {
                SceneManager.UnloadSceneAsync("_Menu");
            };
            LevelLoader.CanActivateLevel = true;
            Destroy(_introPlayer.gameObject, 0);
        }
    }
}