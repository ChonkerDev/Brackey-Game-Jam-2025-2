using System;
using Chonker.Scripts.Management;
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
        if (GameManager.instance.CurrentGameMode == GameManager.GameMode.Campaign) {
            if (SceneManagerWrapper.CurrentSceneId == SceneManagerWrapper.SceneId.Level5) {
                PersistantDataManager.instance.SetCampaignProgress(SceneManagerWrapper.SceneId.Level1);
            }
            else {
                PersistantDataManager.instance.SetCampaignProgress(levelManager.NextScene);
            }
        }
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        
    }

    public override bool CanBeInteractedWith() {
        return levelManager.CanExitLevel;
    }
}
