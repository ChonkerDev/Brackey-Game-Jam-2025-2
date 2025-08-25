using Chonker.Scripts.Player_Raccoon;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerRaccoonComponentContainer : MonoBehaviour
{
    public PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector;

    public PlayerMovementInputWrapper PlayerMovementInputWrapper;

    public PlayerRaccoonController PlayerRaccoonController;

    public PlayerRaccoonView PlayerRaccoonView;

    public void DisablePlayer() {
        PlayerRaccoonController.Disable();
        PlayerRaccoonInteractionDetector.ActivateDetection(false);
        PlayerRaccoonView.HideModel();
    }

    public void EnablePlayer() {
        PlayerRaccoonController.Enable();
        PlayerRaccoonInteractionDetector.ActivateDetection(true);
        PlayerRaccoonView.ShowModel();
    }

}
