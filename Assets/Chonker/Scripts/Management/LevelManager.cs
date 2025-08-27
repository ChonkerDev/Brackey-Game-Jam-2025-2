using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public SceneManagerWrapper.SceneId sceneId;

    public float TimeTaken;
    public bool LevelFinished = false;
    public bool CanExitLevel = false;

    void Start() {
        ScreenFader.FadeIn(1);
    }

    private void Update() {
        if (LevelFinished) return;
        TimeTaken  += Time.deltaTime;
    }
}
