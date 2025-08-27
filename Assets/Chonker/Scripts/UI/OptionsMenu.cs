using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Chonker.Scripts.Management
{
    public class OptionsMenu : MonoBehaviour
    {
        private const string MASTER_AUDIO = "MASTER_AUDIO";
        private const string MUSIC_AUDIO = "MUSIC_AUDIO";
        private const string SFX_AUDIO = "SFX_AUDIO";
        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private Slider MasterVolumeSlider;
        [SerializeField] private Slider MusicVolumeSlider;
        [SerializeField] private Slider SFXVolumeSlider;
        [SerializeField] private Button exitButton;

        private void Awake() {
            bool defaultsNotSet = false;
            if (!PlayerPrefs.HasKey(MASTER_AUDIO)) {
                PlayerPrefs.SetFloat(MASTER_AUDIO, .5f);
                defaultsNotSet = true;
            }

            if (!PlayerPrefs.HasKey(MUSIC_AUDIO)) {
                PlayerPrefs.SetFloat(MUSIC_AUDIO, .5f);
                defaultsNotSet = true;
            }

            if (!PlayerPrefs.HasKey(SFX_AUDIO)) {
                PlayerPrefs.SetFloat(SFX_AUDIO, .5f);
                defaultsNotSet = true;
            }

            if (defaultsNotSet) {
                PlayerPrefs.Save();
            }

            setAudioMixerVolume("MasterVol", PlayerPrefs.GetFloat(MASTER_AUDIO));
            setAudioMixerVolume("MusicVol", PlayerPrefs.GetFloat(MUSIC_AUDIO));
            setAudioMixerVolume("SFXVol", PlayerPrefs.GetFloat(SFX_AUDIO));
        }

        private void Start() {
            MasterVolumeSlider.value = PlayerPrefs.GetFloat(MASTER_AUDIO);
            MusicVolumeSlider.value = PlayerPrefs.GetFloat(MUSIC_AUDIO);
            SFXVolumeSlider.value = PlayerPrefs.GetFloat(SFX_AUDIO);

            MasterVolumeSlider.onValueChanged.AddListener((f) => {
                PlayerPrefs.SetFloat(MASTER_AUDIO, MasterVolumeSlider.value);
                setAudioMixerVolume("MasterVol", f);
                PlayerPrefs.Save();
            });
            MusicVolumeSlider.onValueChanged.AddListener((f) => {
                PlayerPrefs.SetFloat(MUSIC_AUDIO, MasterVolumeSlider.value);
                setAudioMixerVolume("MusicVol", f);
                PlayerPrefs.Save();
            });
            SFXVolumeSlider.onValueChanged.AddListener((f) => {
                PlayerPrefs.SetFloat(SFX_AUDIO, MasterVolumeSlider.value);
                setAudioMixerVolume("SFXVol", f);
                PlayerPrefs.Save();
            });
            
            gameObject.SetActive(false);
        }

        private void setAudioMixerVolume(string name, float val) {
            float scaledValue = Mathf.Max(.0001f, val);
            scaledValue = Mathf.Log10(scaledValue) * 20;
            _audioMixer.SetFloat(name, scaledValue);
        }
    }
}