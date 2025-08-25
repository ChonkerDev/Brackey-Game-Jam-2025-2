using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class RaccoonTrashcan : ProximityInteractable
{
    [SerializeField] private SpriteRenderer _closedSprite;
    [SerializeField] private SpriteRenderer _openSprite;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _openSound;
    [SerializeField] private AudioClip _closeSound;
    void Start() {
        _openSprite.enabled = false;
    }

    public void EnterTrashcan() {
        _closedSprite.enabled = true;
        _openSprite.enabled = false;
        _audioSource.PlayOneShot(_closeSound);
    }

    public void ExitTrashcan() {
        _closedSprite.enabled = false;
        _openSprite.enabled = true;
        _audioSource.PlayOneShot(_openSound);
    }

    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        EnterTrashcan();
    }
}
