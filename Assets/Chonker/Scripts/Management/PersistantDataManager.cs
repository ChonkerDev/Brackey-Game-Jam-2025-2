using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Chonker.Scripts.Management
{
    public class PersistantDataManager : MonoBehaviour
    {
        public static PersistantDataManager instance;
        private const string MASTER_AUDIO = "MASTER_AUDIO";
        private const string MUSIC_AUDIO = "MUSIC_AUDIO";
        private const string SFX_AUDIO = "SFX_AUDIO";
        private const string CAMPAIGN_PROGRESS = "CAMPAIGN_PROGRESS";
        private const string LEVEL_UNLOCKED_SUFFIX = "_LEVEL_UNLOCKED";

        private const string LEVEL_TIME_SUFFIX = "_LEVEL_TIME";
        [SerializeField] private AudioMixer _audioMixer;
        public AudioMixer AudioMixer => _audioMixer;

        private void Awake() {
            if (!instance) {
                instance = this;
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

                foreach (SceneManagerWrapper.SceneId sceneId in SceneManagerWrapper.ValidPlayableLevels) {
                    if (!PlayerPrefs.HasKey(sceneId + LEVEL_TIME_SUFFIX)) {
                        SetLevelTime(sceneId, float.MaxValue);
                        defaultsNotSet = true;
                    }
                }

                if (!PlayerPrefs.HasKey(CAMPAIGN_PROGRESS)) {
                    SetCampaignProgress(SceneManagerWrapper.SceneId.Level1);
                    defaultsNotSet = true;
                }

                if (defaultsNotSet) {
                    PersistData();
                }
            }
        }

        public void PersistData() {
            PlayerPrefs.Save();
        }

        public void StoreMasterVol(float val) {
            PlayerPrefs.SetFloat(MASTER_AUDIO, val);
        }

        public void StoreMusicVol(float val) {
            PlayerPrefs.SetFloat(MUSIC_AUDIO, val);
        }

        public void StoreSFXVol(float val) {
            PlayerPrefs.SetFloat(SFX_AUDIO, val);
        }

        public float GetMasterVol() {
            return PlayerPrefs.GetFloat(MASTER_AUDIO);
        }

        public float GetMusicVol() {
            return PlayerPrefs.GetFloat(MUSIC_AUDIO);
        }

        public float GetSFXVol() {
            return PlayerPrefs.GetFloat(SFX_AUDIO);
        }

        public void SetCampaignProgress(SceneManagerWrapper.SceneId sceneId) {
            PlayerPrefs.SetInt(CAMPAIGN_PROGRESS, (int)sceneId);
            PersistData();
        }

        public SceneManagerWrapper.SceneId GetCampaignProgress() {
            return (SceneManagerWrapper.SceneId)PlayerPrefs.GetInt(CAMPAIGN_PROGRESS);
        }

        public void SetLevelTime(SceneManagerWrapper.SceneId sceneId, float timeInSeconds) {
            PlayerPrefs.SetFloat(sceneId + LEVEL_TIME_SUFFIX, timeInSeconds);
            PersistData();
        }

        public float GetLevelTime(SceneManagerWrapper.SceneId sceneId) {
            return PlayerPrefs.GetFloat(sceneId + LEVEL_TIME_SUFFIX);
        }
    }
}