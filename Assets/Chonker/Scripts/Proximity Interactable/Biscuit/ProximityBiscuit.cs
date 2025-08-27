using System;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class ProximityBiscuit : ProximityInteractable
{
    LevelManager levelManager;
    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        levelManager.NumBiscuitsCollected++;
        gameObject.SetActive(false);
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        
    }
}
