using System;
using UnityEngine;

public class PlayerRaccoonController : MonoBehaviour
{
    [SerializeField] private PlayerRaccoonComponentContainer _playerRaccoonComponentContainer;
    private Rigidbody2D rb;

    private PlayerMovementInputWrapper playerMovementInputWrapper =>
        _playerRaccoonComponentContainer.PlayerMovementInputWrapper;

    private Vector2 movementInput;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2100;
    [SerializeField] private float acceleration = 40;
    CircleCollider2D circleCollider;
    public Vector2 Velocity => rb.linearVelocity;
    public float Radius => circleCollider.radius;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update() {
        movementInput = playerMovementInputWrapper.ReadMovementInput();
    }

    private void FixedUpdate() {
        updateVelocity();
        updateRotation();
    }

    private void updateVelocity() {
        Vector2 targetVelocity = movementInput * maxSpeed;
        rb.linearVelocity = Vector2.MoveTowards(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );
    }

    private void updateRotation() {
        if (movementInput.sqrMagnitude > 0.01f) {
            float targetAngle = Mathf.Atan2(movementInput.y, movementInput.x) * Mathf.Rad2Deg - 90;
            float angle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(angle);
        }
    }

    public void Teleport(Vector2 position) {
        rb.position = position;
    }

    public void SetForward(Vector2 direction) {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        rb.MoveRotation(angle);
    }

    public void Disable() {
        enabled = false;
    }

    public void Enable() {
        enabled = true;
    }
}