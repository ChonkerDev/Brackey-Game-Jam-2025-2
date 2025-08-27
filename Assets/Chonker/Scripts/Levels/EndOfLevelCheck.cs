using System;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class EndOfLevelCheck : ProximityInteractable
{
    private LevelManager levelManager;

    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
    }

    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        if (!levelManager.CanExitLevel) return;
        levelManager.LevelFinished = true;
        Time.timeScale = 0;
        gameObject.SetActive(false);
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        
    }
}
