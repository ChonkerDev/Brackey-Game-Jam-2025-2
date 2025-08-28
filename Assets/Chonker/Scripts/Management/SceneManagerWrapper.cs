using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour
{
    public static SceneManagerWrapper instance;

    public static readonly List<SceneId> ValidPlayableLevels =
        new List<SceneManagerWrapper.SceneId>() {
            SceneId.Level1,
            SceneId.Level2,
            SceneId.Level3,
            SceneId.Level4,
            SceneId.Level5
        };

    private void Awake() {
        if (!instance) {
            instance = this;
        }
    }

    public static void LoadScene(SceneId sceneId) {
        SceneManager.LoadScene((int)sceneId);
    }

    public static bool IsSceneAPlayableLevel(SceneId sceneId) {
        return ValidPlayableLevels.Contains(sceneId);
    }

    public static bool IsCurrentSceneAPlayableLevel() {
        return IsSceneAPlayableLevel(CurrentSceneId);
    }

    public static SceneId GetSceneId(Scene scene) {
        return (SceneId) scene.buildIndex;
    }

    public static SceneId CurrentSceneId => (SceneId)SceneManager.GetActiveScene().buildIndex;

    public enum SceneId
    {
        Bootstrap,
        MainMenu,
        CampaignIntro,
        CampaignOutro,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
    }
}