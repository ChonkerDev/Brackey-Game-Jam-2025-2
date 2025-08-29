using System;
using System.Collections.Generic;
using Chonker.Scripts.Management;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameMode CurrentGameMode;
    [SerializeField] private bool clearTimes;

    public static LayerMask ObstacleLayerMask { get; private set; }
    private void Awake() {
        if (!instance) {
            instance = this;
            ObstacleLayerMask = LayerMask.GetMask("Default");
            Cursor.visible = false;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    private void Update() {
        if (clearTimes) {
            clearTimes = false;
            PersistantDataManager.instance.SetLevelTime(SceneManagerWrapper.SceneId.Level1, float.MaxValue);
            PersistantDataManager.instance.SetLevelTime(SceneManagerWrapper.SceneId.Level2, float.MaxValue);
            PersistantDataManager.instance.SetLevelTime(SceneManagerWrapper.SceneId.Level3, float.MaxValue);
            PersistantDataManager.instance.SetLevelTime(SceneManagerWrapper.SceneId.Level4, float.MaxValue);
            PersistantDataManager.instance.SetLevelTime(SceneManagerWrapper.SceneId.Level5, float.MaxValue);
        }
    }

    public enum GameMode
    {
        Campaign,
        TimeTrial,
    }
}
