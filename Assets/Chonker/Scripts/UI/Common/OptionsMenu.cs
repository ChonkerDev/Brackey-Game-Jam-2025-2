using System;
using Chonker.Core.Attributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Chonker.Scripts.Management
{
    public class OptionsMenu : NavigationUIMenu
    {
        [SerializeField, PrefabModeOnly] private GameObject _optionsMenu;
        [SerializeField, PrefabModeOnly] private Slider MasterVolumeSlider;
        [SerializeField, PrefabModeOnly] private Slider MusicVolumeSlider;
        [SerializeField, PrefabModeOnly] private Slider SFXVolumeSlider;
        [SerializeField, PrefabModeOnly] private Button exitButton;

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

            exitButton.onClick.AddListener(Deactivate);

            Deactivate();
        }

        private void setAudioMixerVolume(string name, float val) {
            float scaledValue = Mathf.Max(.0001f, val);
            scaledValue = Mathf.Log10(scaledValue) * 20;
            PersistantDataManager.instance.AudioMixer.SetFloat(name, scaledValue);
        }
    }
}