using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float TimeTaken;
    public bool LevelFinished = false;
    public bool CanExitLevel = false;
    public SceneManagerWrapper.SceneId NextScene;

    void Start() {
        Time.timeScale = 1f;
        ScreenFader.FadeIn(1);
    }

    private void Update() {
        if (LevelFinished) return;
        TimeTaken  += Time.deltaTime;
    }
}
