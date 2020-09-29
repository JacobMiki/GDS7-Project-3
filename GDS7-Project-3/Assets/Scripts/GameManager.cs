using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace GDS7.Group1.Project3.Assets.Scripts
{
    [Serializable]
    public class Settings
    {
        public float masterVolume = 1f;
        public float sfxVolume = 1f;
        public float musicVolume = 1f;

        public float verticalSensitivity = 1f;
        public float horizontalSensitivity = 1f;
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private AudioMixer _masterMixer;
        [SerializeField] private AudioMixer _gameMixer;
        private GameObject _player;

        public Settings Settings { get; private set; } = new Settings(); 
        public GameObject Player { 
            get
            {
                if(!_player)
                {
                    _player = GameObject.Find("Player");
                }
                return _player;
            }
        }

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void OnEnable()
        {
            if (System.IO.File.Exists(Application.persistentDataPath + "/Settings.json"))
            {
                var settings = System.IO.File.ReadAllText(Application.persistentDataPath + "/Settings.json");
                Settings = JsonUtility.FromJson<Settings>(settings) ?? new Settings();
            }
            StartCoroutine(ApplySoundSettingsCoroutine());
        }

        IEnumerator ApplySoundSettingsCoroutine()
        {
            yield return new WaitForSeconds(0.1f);
            ApplySoundSettings();
        }

        public void ApplySoundSettings()
        {
            _masterMixer.SetFloat("masterVolume", Mathf.Log10(Settings.masterVolume) * 20);
            _gameMixer.SetFloat("sfxVolume", Mathf.Log10(Settings.sfxVolume) * 20);
            _gameMixer.SetFloat("musicVolume", Mathf.Log10(Settings.musicVolume) * 20);
        }

        public void SaveSettings()
        {
            string settings = JsonUtility.ToJson(Settings, true);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/Settings.json", settings);

        }

        private void OnDisable()
        {
            SaveSettings();
        }
    }
}