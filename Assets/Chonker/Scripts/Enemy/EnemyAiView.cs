using System;
using UnityEngine;

namespace Chonker.Scripts.Enemy
{
    public class EnemyAiView : MonoBehaviour
    {
        private EnemyAiController enemyAiController;
        [SerializeField] private EnemyAiStateManager stateManager;
        private Animator animator;

        private int isMovingBoolAnimatorHash;
        private int batSwingTriggerAnimatorHash;
        [SerializeField] private float rotationSpeed = 720;

        private void Awake() {
            enemyAiController = GetComponentInParent<EnemyAiController>();
            animator = GetComponent<Animator>();
        }

        private void Start() {
            isMovingBoolAnimatorHash = Animator.StringToHash("Is Moving");
            batSwingTriggerAnimatorHash = Animator.StringToHash("Swing Bat");
        }

        private void Update() {
            bool isMoving = enemyAiController.Velocity.magnitude > 0.1f;
            animator.SetBool(isMovingBoolAnimatorHash, isMoving);

            if (enemyAiController.Velocity.sqrMagnitude > 0.01f) {
                Vector2 direction = enemyAiController.Velocity;
                float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
        
        public bool IsCurrentOrNextState(string stateName) {
            return animator.GetAnimatorTransitionInfo(0).IsName(stateName) ||
                   animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
        }

        public void TriggerBatSwing() {
            animator.SetTrigger(batSwingTriggerAnimatorHash);
        }

        public Vector2 Forward => transform.right;
    }
}