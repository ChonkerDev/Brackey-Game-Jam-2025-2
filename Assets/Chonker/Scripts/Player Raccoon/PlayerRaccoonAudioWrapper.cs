using System;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;

public class PlayerRaccoonAudioWrapper : MonoBehaviour
{
    [SerializeField] private PlayerRaccoonComponentContainer playerRaccoonComponentContainer;
    private AudioSource audioSource;
    [SerializeField] private AudioClip DeathSoundClip;
    [SerializeField] private AudioSource _skitterSource;
    private LevelManager levelManager;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public void PlayDeathSound() {
        audioSource.PlayOneShot(DeathSoundClip);
    }

    private void Update() {
        if (playerRaccoonComponentContainer.PlayerStateManager.CurrentState == PlayerStateId.Movement &&
            playerRaccoonComponentContainer.PlayerRaccoonController.Velocity.sqrMagnitude > .1f && !PauseMenu.instance.IsPaused && !levelManager.LevelFinished) {
            PlaySkitter();
        }
        else {
            StopSkitter();
        }
    }

    private void PlaySkitter() {
        if (!_skitterSource.isPlaying) {
            _skitterSource.Play();
        }
    }

    private void StopSkitter() {
        if (_skitterSource.isPlaying) {
            _skitterSource.Pause();
        }
    }
}