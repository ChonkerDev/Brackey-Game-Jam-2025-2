using System.Collections;
using Chonker.Scripts.Player_Raccoon;
using UnityEngine;

namespace Chonker.Scripts.Enemy.Enemy_State
{
    public class EnemyStateChase : EnemyAiState
    {
        private Coroutine ChasePlayerC;

        public override void Initialize() {
            base.Initialize();
        }

        public override void OnEnter() {
            ChasePlayerC = StartCoroutine(ChasePlayer());
        }

        public override void OnExit() {
            StopCoroutine(ChasePlayerC);
        }

        public override EnemyStateId StateId => EnemyStateId.Chase;

        public override void ProcessState() {
            if (PlayerRaccoonComponentContainer.PlayerInstance.PlayerStateManager.CurrentState ==
                PlayerStateId.Hidden) {
                StateManager.UpdateStateToPatrol();
            }
        }

        public override void ProcessFixedUpdate() {
            float distanceToPlayer =
                Vector2.Distance(
                    PlayerRaccoonComponentContainer.PlayerInstance.PlayerRaccoonController.transform.position,
                    EnemyAiController.transform.position);
            float distanceThreshold = PlayerRaccoonComponentContainer.PlayerInstance.PlayerRaccoonController.Radius +
                                      EnemyAiController.Radius;
            float additionalDistanceBuffer = .3f;
            if (distanceToPlayer < distanceThreshold + additionalDistanceBuffer) {
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