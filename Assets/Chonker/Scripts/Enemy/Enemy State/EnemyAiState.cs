using Chonker.Runtime.Core.StateMachine;
using Chonker.Scripts.Enemy;
using Chonker.Scripts.Enemy.Enemy_State;
using UnityEditor.MPE;
using UnityEngine;

public abstract class EnemyAiState : StateMachine<EnemyStateId>
{
    protected EnemyAiStateManager StateManager;
    protected EnemyAiController EnemyAiController;
    protected EnemyPlayerDetector EnemyPlayerDetector;
    protected EnemyAiView enemyAiView;
    
    public override void Initialize() {
        EnemyAiController = GetComponentInParent<EnemyAiController>();
        StateManager = GetComponentInParent<EnemyAiStateManager>();
        EnemyPlayerDetector = GetComponentInParent<EnemyPlayerDetector>();
        enemyAiView = transform.root.GetComponentInChildren<EnemyAiView>();
    }
    public abstract void ProcessState();
    public abstract void ProcessFixedUpdate();

}
