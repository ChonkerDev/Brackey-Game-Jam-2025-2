using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float TimeTaken;
    public bool LevelFinished = false;
    public SceneManagerWrapper.SceneId NextScene;
    public int NumBiscuitsCollected;
    private int numBiscuits;
    
    public bool CanExitLevel => numBiscuits == NumBiscuitsCollected;

    private void Awake() {
        numBiscuits = FindObjectsByType<ProximityBiscuit>(FindObjectsSortMode.None).Length;
    }

    void Start() {
        Time.timeScale = 1f;
        ScreenFader.FadeIn(1);
    }

    private void Update() {
        if (LevelFinished) return;
        TimeTaken  += Time.deltaTime;
    }
}
