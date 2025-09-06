using System;
using System.Collections;
using Chonker.Scripts.Management;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class EndOfLevelCheck : ProximityInteractable
{
    private LevelManager levelManager;
    [SerializeField] private TransformBob EndOfLevelIndicator;

    private void Awake() {
        levelManager = FindAnyObjectByType<LevelManager>();
        StartCoroutine(CheckToDisplayEndOfLevelIndicator());
    }

    private void Start() {
        EndOfLevelIndicator.gameObject.SetActive(false);
        StartCoroutine(CheckToDisplayEndOfLevelIndicator());
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

    private IEnumerator CheckToDisplayEndOfLevelIndicator() {

        while (!levelManager.CanExitLevel) {
            yield return null;
        }
        
        EndOfLevelIndicator.gameObject.SetActive(true);
    }

    public override bool CanBeInteractedWith() {
        return levelManager.CanExitLevel;
    }
}
