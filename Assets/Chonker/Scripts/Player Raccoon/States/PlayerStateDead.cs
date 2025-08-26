using Chonker.Runtime.Core.StateMachine;
using UnityEngine;

namespace Chonker.Scripts.Player_Raccoon
{
    public class PlayerStateDead : PlayerState
    {

        public override void OnEnter() {
            playerRaccoonComponentContainer.PlayerRaccoonAudioWrapper.PlayDeathSound();

        }

        public override void OnExit() {
            
        }

        public override void OnFixedUpdate() {
            playerRaccoonController.SetVelocity(Vector2.zero);
        }

        public override void OnUpdate() {
            
        }

        public override PlayerStateId StateId => PlayerStateId.Dead;
    }
}