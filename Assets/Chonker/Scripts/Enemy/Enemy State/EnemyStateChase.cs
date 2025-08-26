using System.Collections;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;

namespace Chonker.Scripts.Enemy.Enemy_State
{
    public class EnemyStateChase : EnemyAiState
    {
        private Coroutine ChasePlayerC;
        private bool exittingState = false;

        public override void Initialize() {
            base.Initialize();
        }

        public override void OnEnter() {
            exittingState = false;
            ChasePlayerC = StartCoroutine(ChasePlayer());
        }

        public override void OnExit() {
            StopCoroutine(ChasePlayerC);
        }

        public override EnemyStateId StateId => EnemyStateId.Chase;

        public override void ProcessState() {
            if (exittingState) return;
            if (PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.CurrentState ==
                PlayerStateId.Hidden || !EnemyPlayerDetector.IsPlayerDetected()) {
                StartCoroutine(DelayPatrolSwitch(2));
            }
        }

        public override void ProcessFixedUpdate() {
            if (exittingState) return;
            float distanceToPlayer =
                Vector2.Distance(
                    PlayerRaccoonComponentContainer.PlayerInstance.PlayerRaccoonController.transform.position,
                    EnemyAiController.transform.position);
            float distanceThreshold = PlayerRaccoonComponentContainer.PlayerInstance.PlayerRaccoonController.Radius +
                                      EnemyAiController.Radius;
            float additionalDistanceBuffer = .3f;
            if (distanceToPlayer < distanceThreshold + additionalDistanceBuffer) {
                enemyAiView.TriggerBatSwing();
                PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.UpdateState(PlayerStateId.Dead);
                StartCoroutine(DelayPatrolSwitch(1));
            }
        }

        private IEnumerator ChasePlayer() {
            while (true) {
                if (EnemyPlayerDetector.IsPlayerDetected()) {
                    Vector2 targetPosition = PlayerRaccoonComponentContainer.PlayerInstance.transform.position;
                    EnemyAiController.setAgentDestination(targetPosition, EnemyAiController.ChaseSpeed);
                }
                yield return new WaitForSecondsRealtime(.1f);
            }
        }

        private IEnumerator DelayPatrolSwitch(float delayTime) {
            exittingState = true;
            EnemyAiController.setAgentDestination(EnemyAiController.transform.position, 0);
            float timer = 0;
            while (true) {
                timer += Time.fixedDeltaTime;
                if (timer > delayTime) {
                    StateManager.UpdateStateToPatrol();
                    break;
                }

                if (EnemyPlayerDetector.IsPlayerDetected()) {
                    exittingState = false;
                    break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }
}