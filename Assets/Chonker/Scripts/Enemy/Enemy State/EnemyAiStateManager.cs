using Chonker.Runtime.Core.StateMachine;
using Chonker.Scripts.Enemy.Enemy_State;
using UnityEngine;

public class EnemyAiStateManager : StateMachineManager<EnemyStateId, EnemyAiState>
{
    public void ProcessCurrentState(EnemyAiController EnemyAiController) {
        GetCurrentState().ProcessState(EnemyAiController);
    }
    
    public void ProcessFixedUpdate(EnemyAiController EnemyAiController) {
        GetCurrentState().ProcessFixedUpdate(EnemyAiController);
    }
}
