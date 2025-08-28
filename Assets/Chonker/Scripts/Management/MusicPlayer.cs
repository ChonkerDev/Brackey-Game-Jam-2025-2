using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance;
    [SerializeField] private AudioSource audioSource;
    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }
}
