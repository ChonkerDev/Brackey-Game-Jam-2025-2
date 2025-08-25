using System;
using Chonker.Scripts.Enemy;
using Chonker.Scripts.Enemy.Enemy_State;
using UnityEngine;

public class EnemyAiController : MonoBehaviour
{
    private EnemyAiStateManager EnemyAiStateManager;
    private EnemyPlayerDetector EnemyPlayerDetector;
    private Rigidbody2D rb;
    public float rotationSpeed = 2100;
    public float PatrolSpeed = 5;
    public float ChaseSpeed = 5;

    private void Awake() {
        EnemyAiStateManager = GetComponentInChildren<EnemyAiStateManager>();
        EnemyPlayerDetector = GetComponentInChildren<EnemyPlayerDetector>();
    }

    void Start() {
        rb.position = ((EnemyStatePatrol)EnemyAiStateManager.GetState(EnemyStateId.Patrol)).PatrolPoints[0]
            .WorldPosition;
    }

    // Update is called once per frame
    void Update() {
        EnemyAiStateManager.ProcessCurrentState(this);
    }

    private void FixedUpdate() {
        EnemyAiStateManager.ProcessFixedUpdate(this);
    }

    public void setVelocity(Vector2 velocity) {
        rb.linearVelocity = velocity;
        if (velocity.sqrMagnitude > 0.01f) {
            setForward(velocity);
        }
    }

    public void setForward(Vector2 direction) {
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        float angle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotationSpeed * Time.deltaTime);
        rb.MoveRotation(angle);
    }
}