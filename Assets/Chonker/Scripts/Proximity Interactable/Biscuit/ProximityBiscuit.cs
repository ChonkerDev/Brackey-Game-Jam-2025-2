using System;
using Chonker.Core.Attributes;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class ProximityBiscuit : ProximityInteractable
{
    LevelManager levelManager;
    [SerializeField, PrefabModeOnly] private AudioSource _CollectedBiscuitAudioSource;
    [SerializeField, PrefabModeOnly] private Collider2D _interactionCollider;
    [SerializeField, PrefabModeOnly] private SpriteRenderer _biscuitSprite;
    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        levelManager.NumBiscuitsCollected++;
        _CollectedBiscuitAudioSource.Play();
        _biscuitSprite.gameObject.SetActive(false);
        _interactionCollider.gameObject.SetActive(false);
        Invoke(nameof(delayTurnOffGameObject), 1);
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        
    }

    private void delayTurnOffGameObject() {
        gameObject.SetActive(false);
    }
}
