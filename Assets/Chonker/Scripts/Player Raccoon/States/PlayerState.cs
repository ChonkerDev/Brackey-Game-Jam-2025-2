using Chonker.Runtime.Core.StateMachine;

namespace Chonker.Scripts.Player_Raccoon
{
    public abstract class PlayerState : StateMachine<PlayerStateId>
    {
        protected PlayerRaccoonComponentContainer playerRaccoonComponentContainer;
        protected PlayerInputWrapper playerMovementInputWrapper => PlayerInputWrapper.instance;
        protected PlayerRaccoonController playerRaccoonController => playerRaccoonComponentContainer.PlayerRaccoonController;
        public override void Initialize() {
            playerRaccoonComponentContainer = GetComponentInParent<PlayerRaccoonComponentContainer>();
        }

        public override void OnEnter() {
            
        }

        public override void OnExit() {
            
        }

        public abstract void OnFixedUpdate();

        public abstract void OnUpdate();

        public override PlayerStateId StateId { get; }
    }
}