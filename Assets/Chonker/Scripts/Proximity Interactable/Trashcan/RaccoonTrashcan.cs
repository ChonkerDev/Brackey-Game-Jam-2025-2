using System;
using System.Collections;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class RaccoonTrashcan : ProximityInteractable
{
    [SerializeField] private SpriteRenderer _closedSprite;
    [SerializeField] private SpriteRenderer _openSprite;
    [SerializeField] private AudioClip _openSound;
    [SerializeField] private AudioClip _closeSound;
    private PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector;
    void Start() {
        _closedSprite.enabled = false;
    }

    public void EnterTrashcan() {
        _closedSprite.enabled = true;
        _openSprite.enabled = false;
        _audioSource.PlayOneShot(_closeSound);
        PlayerRaccoonInteractionDetector.PlayerRaccoonComponentContainer.DisablePlayer();
    }

    public void ExitTrashcan() {
        _closedSprite.enabled = false;
        _openSprite.enabled = true;
        _audioSource.PlayOneShot(_openSound);
        PlayerRaccoonInteractionDetector.PlayerRaccoonComponentContainer.EnablePlayer();
        Vector2 targetDirection = PlayerRaccoonInteractionDetector.PlayerRaccoonComponentContainer.transform.position - transform.position;
        PlayerRaccoonInteractionDetector.PlayerRaccoonComponentContainer.PlayerRaccoonController.SetForward(targetDirection);
    }

    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        this.PlayerRaccoonInteractionDetector  = PlayerRaccoonInteractionDetector;
        EnterTrashcan();
        StartCoroutine(processTrashCan());
    }

    private IEnumerator processTrashCan() {
        yield return null; // need to eat a frame so input isn't carried over
        while (!PlayerRaccoonInteractionDetector.PlayerRaccoonComponentContainer.PlayerMovementInputWrapper
               .WasInteractPressed()) {
            yield return null;
        }
        ExitTrashcan();
        PlayerRaccoonInteractionDetector = null;
    }
}
