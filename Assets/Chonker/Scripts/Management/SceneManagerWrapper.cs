using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour
{
    public static SceneManagerWrapper instance;

    private void Awake() {
        instance = this;
    }

    public static void LoadScene(SceneId sceneId) {
        SceneManager.LoadScene((int) sceneId);
    }

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
