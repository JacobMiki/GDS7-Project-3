using System.Collections;
using System.Collections.Generic;
using GDS7.Group1.Project3.Assets.Scripts.Scene;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Selectable _firstSelected;

        [Header("Options")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;
        [SerializeField] private Slider _horiSensitivitySlider;
        [SerializeField] private Slider _vertSensitivitySlider;

        [Header("Credits")]
        [SerializeField] private Credits _credits;

        [Header("Video")]
        [SerializeField] private VideoPlayer _introPlayer;
        [SerializeField] private GameObject _canSkipText;

        [Header("Level")]
        [SerializeField] private Level _startingLevel;

        private PlayerInputActions _inputActions;
        private bool _settingsInit = false;


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

            _settingsInit = true;
            _masterVolumeSlider.value = GameManager.Instance.Settings.masterVolume;
            _sfxVolumeSlider.value = GameManager.Instance.Settings.sfxVolume;
            _musicVolumeSlider.value = GameManager.Instance.Settings.musicVolume;
            _horiSensitivitySlider.value = GameManager.Instance.Settings.horizontalSensitivity;
            _vertSensitivitySlider.value = GameManager.Instance.Settings.verticalSensitivity;
            _settingsInit = false;

            GameManager.Instance.ApplySoundSettings();
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
            GameManager.Instance.SaveSettings();
            _animator.SetTrigger("Outro");
            _introPlayer.loopPointReached += _introPlayer_loopPointReached;
            _introPlayer.targetCamera = Camera.main;
            _introPlayer.Play();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _inputActions.Player.Interact.performed += SkipIntroOnInteract;
            _inputActions.Enable();
            StartCoroutine(WaitForLevelReady());
            Application.backgroundLoadingPriority = ThreadPriority.Low;
            StartCoroutine(LevelLoader.LoadLevel(_startingLevel));
        }

        public void Quit()
        {
            GameManager.Instance.SaveSettings();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }

        public void ToOptions()
        {
            _animator.SetTrigger("ToOptions");
        }

        public void ToMain()
        {
            _animator.SetTrigger("ToMain");
        }

        public void RollCredits()
        {
            _credits.onFinish += _credits_onFinish;
            _inputActions.Player.Interact.performed += SkipCreditsOnInteract;
            _inputActions.Enable();
            _credits.gameObject.SetActive(true);
        }

        private void SkipCreditsOnInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _credits.gameObject.SetActive(false);
            _credits.onFinish -= _credits_onFinish;
            _inputActions.Disable();
            _inputActions.Player.Interact.performed -= SkipCreditsOnInteract;
        }

        private void _credits_onFinish()
        {
            _credits.onFinish -= _credits_onFinish;
        }

        public void OnMasterVolumeChange(float value)
        {
            if (_settingsInit)
            {
                return;
            }
            GameManager.Instance.Settings.masterVolume = value;
            GameManager.Instance.ApplySoundSettings();
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        public void OnSfxVolumeChange(float value)
        {
            if (_settingsInit)
            {
                return;
            }
            GameManager.Instance.Settings.sfxVolume = value;
            GameManager.Instance.ApplySoundSettings();
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }

        public void OnMusicVolumeChange(float value)
        {
            if (_settingsInit)
            {
                return;
            }
            GameManager.Instance.Settings.musicVolume = value;
            GameManager.Instance.ApplySoundSettings();
        }

        public void OnVertSensChange(float value)
        {
            if (_settingsInit)
            {
                return;
            }
            GameManager.Instance.Settings.verticalSensitivity = value;
        }

        public void OnHoriSensChange(float value)
        {
            if (_settingsInit)
            {
                return;
            }
            GameManager.Instance.Settings.horizontalSensitivity = value;
        }

        private void SkipIntroOnInteract(UnityEngine.InputSystem.InputAction.CallbackContext obj)
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
                _inputActions.Player.Interact.performed -= SkipIntroOnInteract;
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
            SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
            LevelLoader.CanActivateLevel = true;
            Destroy(_introPlayer.gameObject, 0);
        }

        private void SceneManager_activeSceneChanged(UnityEngine.SceneManagement.Scene arg0, UnityEngine.SceneManagement.Scene arg1)
        {
            SceneManager.activeSceneChanged -= SceneManager_activeSceneChanged;
            LevelLoader.CanActivateLevel = false;
            SceneManager.UnloadSceneAsync("_Menu");
        }
    }
}