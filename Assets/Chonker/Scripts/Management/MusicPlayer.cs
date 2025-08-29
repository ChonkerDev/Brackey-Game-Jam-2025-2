using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    [SerializeField] private AudioSource audioSource0;
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private AudioSource audioSource3;
    [SerializeField] private AudioSource audioSource4;
    private SceneManagerWrapper.SceneId prevSceneId;

    private void Awake() {
        if (!instance) {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SceneManagerWrapper.SceneId currentSceneId = SceneManagerWrapper.GetSceneId(scene);
        if (!SceneManagerWrapper.IsSceneAPlayableLevel(prevSceneId) &&
            SceneManagerWrapper.IsSceneAPlayableLevel(currentSceneId)) {
            audioSource0.Play();
            audioSource1.Play();
            audioSource2.Play();
            audioSource3.Play();
            audioSource4.Play();
        }

        if (!SceneManagerWrapper.IsSceneAPlayableLevel(currentSceneId)) {
            audioSource0.Stop();
            audioSource1.Stop();
            audioSource2.Stop();
            audioSource3.Stop();
            audioSource4.Stop();
        }

        if (SceneManagerWrapper.IsSceneAPlayableLevel(currentSceneId)) {
            switch (currentSceneId) {
                case SceneManagerWrapper.SceneId.Level1:
                    audioSource0.volume = 1;
                    audioSource1.volume = 0;
                    audioSource2.volume = 0;
                    audioSource3.volume = 0;
                    audioSource4.volume = 0;
                    break;
                case SceneManagerWrapper.SceneId.Level2:
                    audioSource0.volume = 1;
                    audioSource1.volume = 1;
                    audioSource2.volume = 0;
                    audioSource3.volume = 0;
                    audioSource4.volume = 0;

                    break;
                case SceneManagerWrapper.SceneId.Level3:
                    audioSource0.volume = 1;
                    audioSource1.volume = 1;
                    audioSource2.volume = 1;
                    audioSource3.volume = 0;
                    audioSource4.volume = 0;

                    break;
                case SceneManagerWrapper.SceneId.Level4:
                    audioSource0.volume = 1;
                    audioSource1.volume = 1;
                    audioSource2.volume = 1;
                    audioSource3.volume = 1;
                    audioSource4.volume = 0;

                    break;
                case SceneManagerWrapper.SceneId.Level5:
                    audioSource0.volume = 1;
                    audioSource1.volume = 1;
                    audioSource2.volume = 1;
                    audioSource3.volume = 1;
                    audioSource4.volume = 1;

                    break;
                default:
                    audioSource0.Stop();
                    audioSource0.Stop();
                    audioSource0.Stop();
                    audioSource0.Stop();
                    audioSource0.Stop();
                    break;
            }
        }

        prevSceneId = currentSceneId;
    }
}