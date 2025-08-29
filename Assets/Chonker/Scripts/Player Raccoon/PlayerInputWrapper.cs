using System;
using UnityEngine;

public class PlayerInputWrapper : MonoBehaviour
{
    private IAPlayerControl inputActions;
    public static PlayerInputWrapper instance;
    private void Awake() {
        if (!instance) {
            inputActions = new();
            instance = this;
            inputActions.Enable();
        }
    }

    public Vector2 ReadMovementInput() {
        return inputActions.PlayerControl.Movement.ReadValue<Vector2>();
    }

    public bool WasInteractPressed() {
        return inputActions.PlayerControl.Interaction.WasPressedThisFrame();
    }

    public bool wasDialogueProceedPressedThisFrame() {
        return inputActions.PlayerControl.Interaction.WasPressedThisFrame();
    }

    public bool wasPausePressedThisFrame() {
        return inputActions.UI.PausePressed.WasPressedThisFrame();
    }
    
    public bool wasExitMenuPressedThisFrame() {
        return inputActions.UI.ExitMenu.WasPressedThisFrame();
    }

    public bool wasNavigateLeftPressedThisFrame() {
        return inputActions.UI.NavigateLeft.WasPressedThisFrame();
    }

    public bool wasNavigateRightPressedThisFrame() {
        return inputActions.UI.NavigateRight.WasPressedThisFrame();
    }
}