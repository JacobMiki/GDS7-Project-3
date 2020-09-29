using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    public class TheEndTrigger : MonoBehaviour
    {
        [SerializeField] private string _endScreenName = "The End Screen";
        [SerializeField] private string _creditsName = "Credits";
        [SerializeField] private string _blackOverlayName = "Black background";
        private GameObject _screens;
        private GameObject _endScreen;
        private GameObject _creditsScreen;
        private GameObject _blackOverlay;
        private VideoPlayer _videoPlayer;
        private Credits _credits;
        private PlayerInputActions _inputActions;

        private void OnEnable()
        {
            _screens = GameObject.FindWithTag("Screens");
            _endScreen = _screens.transform.Find(_endScreenName).gameObject;
            _creditsScreen = _screens.transform.Find(_creditsName).gameObject;
            _blackOverlay = _screens.transform.Find(_blackOverlayName).gameObject;
            _videoPlayer = _endScreen.GetComponent<VideoPlayer>();
            _credits = _creditsScreen.GetComponent<Credits>();
        }

        public void Trigger()
        {
            GameObject.FindWithTag("Player").GetComponent<CharacterInput>().InputsEnabled = false;

            _inputActions = new PlayerInputActions();
            _inputActions.Player.Interact.performed += SkipVideo;
            _inputActions.Enable();
            _endScreen.SetActive(true);
            _videoPlayer.loopPointReached += VideoPlayer_loopPointReached;
        }

        private void SkipVideo(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            VideoEnd();
        }

        private void VideoPlayer_loopPointReached(VideoPlayer source)
        {
            VideoEnd();
        }

        private void VideoEnd()
        {
            _inputActions.Player.Interact.performed -= SkipVideo;
            _inputActions.Player.Interact.performed += SkipCredits;
            _endScreen.SetActive(false);
            _blackOverlay.SetActive(true);
            _credits.gameObject.SetActive(true);
            _credits.onFinish += _credits_onFinish;
        }

        private void SkipCredits(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _credits_onFinish();
        }

        private void _credits_onFinish()
        {
            _inputActions.Player.Interact.performed -= SkipCredits;
            _inputActions.Disable();
            SceneManager.LoadScene("_Master");
        }
    }
}