using System;
using System.Collections;
using Chonker.Core.Attributes;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Chonker.Scripts.Management
{
    public class OptionsMenu : NavigationUIMenu
    {
        [SerializeField] private AudioSource _audioSourceSliderSFX;
        [SerializeField, PrefabModeOnly] private GameObject _optionsMenu;
        [SerializeField, PrefabModeOnly] private Slider MasterVolumeSlider;
        [SerializeField, PrefabModeOnly] private Slider MusicVolumeSlider;
        [SerializeField, PrefabModeOnly] private Slider SFXVolumeSlider;
        [SerializeField, PrefabModeOnly] private Button exitButton;


        protected override void OnAwake() {

        }

        private void Start() {
            MasterVolumeSlider.value = PersistantDataManager.instance.GetMasterVol();
            MusicVolumeSlider.value = PersistantDataManager.instance.GetMusicVol();
            SFXVolumeSlider.value = PersistantDataManager.instance.GetSFXVol();
            
            setAudioMixerVolume("MasterVol", PersistantDataManager.instance.GetMasterVol());
            setAudioMixerVolume("MusicVol", PersistantDataManager.instance.GetMusicVol());
            setAudioMixerVolume("SFXVol", PersistantDataManager.instance.GetSFXVol());

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
                _audioSourceSliderSFX.Play();
            });

            exitButton.onClick.AddListener(Deactivate);

            Deactivate();
        }

        private void setAudioMixerVolume(string name, float val) {
            float scaledValue = Mathf.Max(.0001f, val);
            scaledValue = Mathf.Log10(scaledValue) * 20;
            PersistantDataManager.instance.AudioMixer.SetFloat(name, scaledValue);
        }

        protected override void processCurrentMenu() {
            if (PlayerInputWrapper.instance.wasExitMenuPressedThisFrame()) {
                StartCoroutine(DelayExit());
            }
        }

        private IEnumerator DelayExit() {
            yield return null; // eat a frame so input doesn't carry over to next menu
            Deactivate();
        }
    }
}