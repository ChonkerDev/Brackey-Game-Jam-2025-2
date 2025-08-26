using Chonker.Runtime.Core.StateMachine;
using Chonker.Scripts.Enemy.Enemy_State;
using UnityEditor.MPE;
using UnityEngine;

public abstract class EnemyAiState : StateMachine<EnemyStateId>
{
    protected EnemyAiStateManager StateManager;
    protected EnemyAiController EnemyAiController;
    public override void Initialize() {
        EnemyAiController = GetComponentInParent<EnemyAiController>();
        StateManager = GetComponentInParent<EnemyAiStateManager>();
    }
    public abstract void ProcessState();
    public abstract void ProcessFixedUpdate();

}
