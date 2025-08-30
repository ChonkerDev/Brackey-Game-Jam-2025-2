using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Chonker.Scripts.Enemy.Enemy_State
{
    public class EnemyStatePatrol : EnemyAiState
    {
        public EnemyPatrolPointData[] PatrolPoints;
        private Coroutine processPatrolC;

        public override void Initialize() {
            base.Initialize();
            PatrolPoints = GetComponentsInChildren<EnemyPatrolPoint>()
                .Select((patrolPoint) => patrolPoint.GeneratePatrolPointData()).ToArray();
        }

        public override void OnEnter() {
            processPatrolC = StartCoroutine(processPatrol(FindClosestPatrolPoint()));
        }

        public override void OnExit() {
            StopCoroutine(processPatrolC);
        }

        public override EnemyStateId StateId => EnemyStateId.Patrol;

        public override void ProcessState() {
        }

        public override void ProcessFixedUpdate() {
            if (EnemyPlayerDetector.IsPlayerDetected()) {
                StateManager.UpdateStateToChase();
            }
        }

        private IEnumerator processPatrol(int startingIndex) {
            int currentPatrolPointTarget = startingIndex;
            float waitTimer = 0;
            EnemyAiController.setAgentDestination(EnemyAiController.transform.position, EnemyAiController.PatrolSpeed);
            yield return new WaitForSeconds(3);
            EnemyPatrolPointData currentPointTarget = PatrolPoints[currentPatrolPointTarget];
            EnemyAiController.setAgentDestination(currentPointTarget.WorldPosition, EnemyAiController.PatrolSpeed);
            while (true) {
                float distanceToTarget =
                    Vector2.Distance(EnemyAiController.transform.position, currentPointTarget.WorldPosition);
                if (distanceToTarget < .1f) {
                    waitTimer += Time.fixedDeltaTime;
                }

                if (waitTimer >= currentPointTarget.moveToNextPositionDelayInSeconds) {
                    currentPatrolPointTarget++;
                    if (currentPatrolPointTarget >= PatrolPoints.Length) {
                        currentPatrolPointTarget = 0;
                    }

                    currentPointTarget = PatrolPoints[currentPatrolPointTarget];
                    waitTimer = 0;
                    EnemyAiController.setAgentDestination(currentPointTarget.WorldPosition,
                        EnemyAiController.PatrolSpeed);
                }

                yield return new WaitForFixedUpdate();
            }
        }

        private int FindClosestPatrolPoint() {
            int closestPatrolPoint = 0;
            for (var i = 1; i < PatrolPoints.Length; i++) {
                EnemyPatrolPointData  prevPatrolPoint = PatrolPoints[i - 1];
                EnemyPatrolPointData  currentPatrolPoint = PatrolPoints[i];
                float distanceToPrevPatrolPoint = Vector2.Distance(prevPatrolPoint.WorldPosition, transform.position);
                float distanceToCurrentPatrolPoint = Vector2.Distance(currentPatrolPoint.WorldPosition, transform.position);
                if (distanceToCurrentPatrolPoint < distanceToPrevPatrolPoint) {
                    closestPatrolPoint = i;
                }
            }

            return closestPatrolPoint;
        }
        
    }
}