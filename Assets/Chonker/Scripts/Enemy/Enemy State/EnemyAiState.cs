using Chonker.Runtime.Core.StateMachine;
using Chonker.Scripts.Enemy.Enemy_State;
using UnityEditor.MPE;
using UnityEngine;

public abstract class EnemyAiState : StateMachine<EnemyStateId>
{
    public abstract void ProcessState(EnemyAiController EnemyAiController);
    public abstract void ProcessFixedUpdate(EnemyAiController EnemyAiController);

}
