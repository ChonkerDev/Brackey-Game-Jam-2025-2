using System;
using UnityEngine;

public class RaccoonPlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    PlayerMovementInputWrapper playerMovementInputWrapper;
    private Vector2 movementInput;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2100;
    [SerializeField] private float acceleration = 20;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        playerMovementInputWrapper = GetComponent<PlayerMovementInputWrapper>();
        rb.freezeRotation = true;
    }

    void Start() {
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
}