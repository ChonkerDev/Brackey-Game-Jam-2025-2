using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour
{
    public static SceneManagerWrapper instance;

    private void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public static void LoadScene(SceneId sceneId) {
        SceneManager.LoadScene((int) sceneId);
    }

    public enum SceneId
    {
        Bootstrap,
        MainMenu
    }
}
