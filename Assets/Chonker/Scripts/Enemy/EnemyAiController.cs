using System;
using Chonker.Scripts.Enemy;
using Chonker.Scripts.Enemy.Enemy_State;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAiController : MonoBehaviour
{
    private EnemyAiStateManager EnemyAiStateManager;
    private EnemyPlayerDetector EnemyPlayerDetector;
    private Rigidbody2D rb;
    public float PatrolSpeed = 5;
    public float ChaseSpeed = 5;
    private NavMeshAgent agent;
    private CircleCollider2D circleCollider;

    public float Radius => circleCollider.radius;
    public Vector2 Velocity => agent.velocity;
    private void Awake() {
        EnemyAiStateManager = GetComponentInChildren<EnemyAiStateManager>();
        EnemyPlayerDetector = GetComponentInChildren<EnemyPlayerDetector>();
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        circleCollider =  GetComponent<CircleCollider2D>();
    }

    void Start() {
        agent.radius = Radius;
        agent.updateRotation = true;
        rb.position = ((EnemyStatePatrol)EnemyAiStateManager.GetState(EnemyStateId.Patrol)).PatrolPoints[0]
            .WorldPosition;
    }

    // Update is called once per frame
    void Update() {
        EnemyAiStateManager.ProcessCurrentState();
    }

    private void FixedUpdate() {
        EnemyAiStateManager.ProcessFixedUpdate();
    }

    public void setAgentDestination(Vector2 destination, float speed) {
        agent.speed = speed;
        agent.SetDestination(destination);
    }
}