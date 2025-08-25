using System;
using UnityEngine;

public class PlayerMovementInputWrapper : MonoBehaviour
{
    private IAPlayerControl inputActions;

    private void Awake() {
        inputActions = new IAPlayerControl();
        inputActions.PlayerControl.Enable();
    }

    public Vector2 ReadMovementInput() {
        return inputActions.PlayerControl.Movement.ReadValue<Vector2>();
    }
}