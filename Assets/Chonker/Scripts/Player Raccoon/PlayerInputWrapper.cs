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
}