using System;
using UnityEngine;

public class PlayerRaccoonController : MonoBehaviour
{
    [SerializeField] private PlayerRaccoonComponentContainer _playerRaccoonComponentContainer;
    private Rigidbody2D rb;
    private PlayerMovementInputWrapper playerMovementInputWrapper =>
        _playerRaccoonComponentContainer.PlayerMovementInputWrapper;

    public float maxSpeed = 5f;
    public float rotationSpeed = 2100;
    public float acceleration = 40;
    CircleCollider2D circleCollider;
    public Vector2 Velocity => rb.linearVelocity;
    public float Rotation => rb.rotation;
    public float Radius => circleCollider.radius;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        _playerRaccoonComponentContainer.PlayerStateManager.GetCurrentState().OnUpdate();

    }

    private void FixedUpdate() {
        _playerRaccoonComponentContainer.PlayerStateManager.GetCurrentState().OnFixedUpdate();
    }


    public void SetVelocity(Vector2 velocity) {
        rb.linearVelocity = velocity;
    }

    public void Teleport(Vector2 position) {
        rb.position = position;
    }

    public void SetRotation(float angle) {
        rb.MoveRotation(angle);
    }

    public void SetForward(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.MoveRotation(angle);
    }
    
    public void EnablePhysicsCollider(bool enabled) {
        circleCollider.enabled = enabled;
    }
}