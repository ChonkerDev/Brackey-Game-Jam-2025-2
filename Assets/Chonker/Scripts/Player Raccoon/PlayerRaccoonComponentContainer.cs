using Chonker.Scripts.Player_Raccoon;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerRaccoonComponentContainer : MonoBehaviour
{
    public PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector;

    public PlayerMovementInputWrapper PlayerMovementInputWrapper;

    public PlayerRaccoonController PlayerRaccoonController;

    public PlayerRaccoonView PlayerRaccoonView;

    public void HidePlayer() {
        PlayerRaccoonInteractionDetector.ActivateDetection(false);
        PlayerRaccoonView.HideModel();
    }

    public void ShowPlayer() {
        PlayerRaccoonInteractionDetector.ActivateDetection(true);
        PlayerRaccoonView.ShowModel();
    }

}
