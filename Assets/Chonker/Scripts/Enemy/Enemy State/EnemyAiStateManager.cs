using Chonker.Runtime.Core.StateMachine;
using Chonker.Scripts.Enemy.Enemy_State;
using UnityEngine;

public class EnemyAiStateManager : StateMachineManager<EnemyStateId, EnemyAiState>
{
    public void ProcessCurrentState() {
        GetCurrentState().ProcessState();
    }
    
    public void ProcessFixedUpdate() {
        GetCurrentState().ProcessFixedUpdate();
    }
}
