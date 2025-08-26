using Chonker.Runtime.Core.StateMachine;

namespace Chonker.Scripts.Player_Raccoon
{
    public class PlayerStateManager : StateMachineManager<PlayerStateId, PlayerState>
    {
        public void UpdateState(PlayerStateId stateId) {
            base.UpdateState(stateId);
        }
    }
}