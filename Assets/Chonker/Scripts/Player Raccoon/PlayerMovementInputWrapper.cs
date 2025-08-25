using System;
using UnityEngine;

public class PlayerMovementInputWrapper : MonoBehaviour
{
    private IAPlayerControl inputActions;

    private void Awake() {
        inputActions = new();
    }

    private void OnDestroy() {
        inputActions.Dispose();
    }

    private void OnEnable() {
        inputActions.PlayerControl.Enable();
    }

    private void OnDisable() {
        inputActions.PlayerControl.Disable();
    }

    public Vector2 ReadMovementInput() {
        return inputActions.PlayerControl.Movement.ReadValue<Vector2>();
    }

    public bool WasInteractPressed() {
        return inputActions.PlayerControl.Interaction.WasPressedThisFrame();
    }
}