using System.Linq;
using UnityEngine;

namespace Chonker.Scripts.Enemy.Enemy_State
{
    public class EnemyStatePatrol : EnemyAiState
    {
        [SerializeField] private float patrolSpeed;
        public EnemyPatrolPointData[] PatrolPoints;
        public override void Initialize() {
            PatrolPoints = GetComponentsInChildren<EnemyPatrolPoint>().Select((patrolPoint) => patrolPoint.GeneratePatrolPointData()).ToArray();
        }

        public override void OnEnter() {
            
        }

        public override void OnExit() {
            
        }

        public override EnemyStateId StateId => EnemyStateId.Patrol;
        public override void ProcessState(EnemyAiController EnemyAiController) {
            
        }

        public override void ProcessFixedUpdate(EnemyAiController EnemyAiController) {
            
        }
    }
}