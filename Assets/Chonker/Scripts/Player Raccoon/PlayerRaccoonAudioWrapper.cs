using System;
using UnityEngine;

public class PlayerRaccoonAudioWrapper : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip DeathSoundClip;
    [SerializeField] private AudioSource _skitterSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound() {
        audioSource.PlayOneShot(DeathSoundClip);
    }

    public void PlaySkitter() {
        if (!_skitterSource.isPlaying) {
            _skitterSource.Play();
        }
    }

    public void StopSkitter() {
        if (_skitterSource.isPlaying) {
            _skitterSource.Pause();
        }
    }
}