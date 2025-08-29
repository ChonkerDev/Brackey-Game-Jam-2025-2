using System;
using System.Collections.Generic;
using Chonker.Scripts.Management;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameMode CurrentGameMode;
    [SerializeField] private bool resetPlayerData;

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
        if (resetPlayerData) {
            resetPlayerData = false;
            PlayerPrefs.DeleteAll();
        }
    }

    public enum GameMode
    {
        Campaign,
        TimeTrial,
    }
}
