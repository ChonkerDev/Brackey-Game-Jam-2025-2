using System;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerRaccoonComponentContainer : MonoBehaviour
{
    public PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector;
    public PlayerRaccoonController PlayerRaccoonController;
    public PlayerRaccoonView PlayerRaccoonView;
    public PlayerRaccoonAudioWrapper PlayerRaccoonAudioWrapper;
    public PlayerStateManager PlayerStateManager;

    public static PlayerRaccoonComponentContainer PlayerInstance;

    private void Awake() {
        PlayerInstance = this;
    }

    private void OnDestroy() {
        PlayerInstance = null;
    }
    
}