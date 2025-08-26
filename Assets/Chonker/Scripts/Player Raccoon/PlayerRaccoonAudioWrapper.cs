using System;
using UnityEngine;

public class PlayerRaccoonAudioWrapper : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip DeathSoundClip;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDeathSound() {
        audioSource.PlayOneShot(DeathSoundClip);
    }
}