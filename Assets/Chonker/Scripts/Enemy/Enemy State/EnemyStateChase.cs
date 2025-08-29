using System.Collections;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;

namespace Chonker.Scripts.Enemy.Enemy_State
{
    public class EnemyStateChase : EnemyAiState
    {
        private float lostTrackOfPlayerTime;
        private float lostTrackOfPlayerTimeout = 3;

        public override void Initialize() {
            base.Initialize();
        }

        public override void OnEnter() {
            StartCoroutine(ChasePlayer());
            lostTrackOfPlayerTime = 0;
        }

        public override void OnExit() {
            StopAllCoroutines();
        }

        public override EnemyStateId StateId => EnemyStateId.Chase;

        public override void ProcessState() {
            if (PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.CurrentState ==
                PlayerStateId.Hidden || lostTrackOfPlayerTime > lostTrackOfPlayerTimeout) {
                StateManager.UpdateStateToPatrol();
            }
        }

        public override void ProcessFixedUpdate() {
            bool canSeePlayer = EnemyPlayerDetector.IsPlayerDetected();
            if (canSeePlayer) {
                lostTrackOfPlayerTime = 0;
            }
            else {
                lostTrackOfPlayerTime += Time.deltaTime;
            }

            float distanceToPlayer =
                Vector2.Distance(
                    PlayerRaccoonComponentContainer.PlayerInstance.PlayerRaccoonController.transform.position,
                    EnemyAiController.transform.position);
            float distanceThreshold = PlayerRaccoonComponentContainer.PlayerInstance.PlayerRaccoonController.Radius +
                                      EnemyAiController.Radius;
            float additionalDistanceBuffer = .3f;
            if (distanceToPlayer < distanceThreshold + additionalDistanceBuffer && EnemyPlayerDetector.IsPlayerDetected()) {
                enemyAiView.TriggerBatSwing();
                PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.UpdateState(PlayerStateId.Dead);
                StateManager.UpdateStateToPatrol();
            }
        }

        private IEnumerator ChasePlayer() {
            while (true) {
                Vector2 targetPosition = PlayerRaccoonComponentContainer.PlayerInstance.transform.position;
                EnemyAiController.setAgentDestination(targetPosition, EnemyAiController.ChaseSpeed);
                yield return new WaitForSecondsRealtime(.1f);
            }
        }
    }
}