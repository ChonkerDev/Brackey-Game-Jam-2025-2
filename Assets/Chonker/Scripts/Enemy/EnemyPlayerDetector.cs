using System;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;

namespace Chonker.Scripts.Enemy
{
    public class EnemyPlayerDetector : MonoBehaviour
    {
        [SerializeField] private EnemyAiView enemyAiView;
        private bool detectedPlayer = false;
        [SerializeField] private float detectionDistance;
        [SerializeField] private float visionHalfAngle;

        private LayerMask ValidBlockingLayers;
        private int playerLayer;

        private void Start() {
            ValidBlockingLayers = LayerMask.GetMask("Player", "Default");
            playerLayer = LayerMask.NameToLayer("Player");
        }

        private void FixedUpdate() {
            PlayerRaccoonComponentContainer PlayerInstance = PlayerRaccoonComponentContainer.PlayerInstance;
            Vector2 direction = PlayerInstance.transform.position - transform.position;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, detectionDistance, ValidBlockingLayers, -1000, 10);
            if (hit.transform && hit.collider.gameObject.layer == playerLayer) {
                float angleToPlayer = Vector2.Angle(direction, enemyAiView.Forward);
                if (angleToPlayer < visionHalfAngle) {
                    detectedPlayer = true;
                    Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.green);
                    return;
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * detectionDistance, Color.red);
            detectedPlayer = false;
        }

        public bool IsPlayerDetected() {
            PlayerRaccoonComponentContainer PlayerInstance = PlayerRaccoonComponentContainer.PlayerInstance;
            if (PlayerInstance.PlayerStateManager.CurrentState == PlayerStateId.Dead) return false;
            return detectedPlayer;
        }
    }
}