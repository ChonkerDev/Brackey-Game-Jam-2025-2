using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Chonker.Scripts.Management
{
    public class OptionsMenu : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;

        [SerializeField] private Slider MasterVolumeSlider;
        [SerializeField] private Slider MusicVolumeSlider;
        [SerializeField] private Slider SFXVolumeSlider;
        [SerializeField] private Button exitButton;

        private void Start() {
            setAudioMixerVolume("MasterVol", PersistantDataManager.instance.GetMasterVol());
            setAudioMixerVolume("MusicVol", PersistantDataManager.instance.GetMusicVol());
            setAudioMixerVolume("SFXVol", PersistantDataManager.instance.GetSFXVol());

            MasterVolumeSlider.value = PersistantDataManager.instance.GetMasterVol();
            MusicVolumeSlider.value = PersistantDataManager.instance.GetMusicVol();
            SFXVolumeSlider.value = PersistantDataManager.instance.GetSFXVol();

            MasterVolumeSlider.onValueChanged.AddListener((f) => {
                PersistantDataManager.instance.StoreMasterVol(f);
                setAudioMixerVolume("MasterVol", f);
                PersistantDataManager.instance.PersistData();
            });
            MusicVolumeSlider.onValueChanged.AddListener((f) => {
                PersistantDataManager.instance.StoreMusicVol(f);
                setAudioMixerVolume("MusicVol", f);
                PersistantDataManager.instance.PersistData();
            });
            SFXVolumeSlider.onValueChanged.AddListener((f) => {
                PersistantDataManager.instance.StoreSFXVol(f);
                setAudioMixerVolume("SFXVol", f);
                PersistantDataManager.instance.PersistData();
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