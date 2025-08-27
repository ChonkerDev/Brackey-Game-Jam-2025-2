using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public float TimeTaken;
    public bool LevelFinished = false;
    public SceneManagerWrapper.SceneId NextScene;
    private int numBiscuitsCollected = 0;

    public int NumBiscuitsCollected {
        get { return numBiscuitsCollected; }
        set {
            numBiscuitsCollected = value;
            OnBiscuitCollected.Invoke(numBiscuitsCollected);
        }
    }
    public int numBiscuits { get; private set; }
    
    public bool CanExitLevel => numBiscuits == NumBiscuitsCollected;
    public UnityEvent<int> OnBiscuitCollected;

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
