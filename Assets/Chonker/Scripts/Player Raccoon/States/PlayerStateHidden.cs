using Chonker.Runtime.Core.StateMachine;
using UnityEngine;

namespace Chonker.Scripts.Player_Raccoon
{
    public class PlayerStateHidden : PlayerState
    {
        public RaccoonTrashcan CurrentTrashCan;

        public override void OnEnter() {
            CurrentTrashCan.EnterTrashcan(playerRaccoonComponentContainer);
            playerRaccoonComponentContainer.PlayerRaccoonView.HideModel();
            playerRaccoonController.EnablePhysicsCollider(false);
        }

        public override void OnExit() {
            playerRaccoonComponentContainer.PlayerRaccoonView.ShowModel();
            CurrentTrashCan.ExitTrashcan();
            Vector2 targetDirection = playerRaccoonComponentContainer.transform.position - CurrentTrashCan.transform.position;
            playerRaccoonComponentContainer.PlayerRaccoonController.SetForward(targetDirection);
            playerRaccoonController.EnablePhysicsCollider(true);
        }

        public override void OnFixedUpdate() {
            playerRaccoonController.SetVelocity(Vector2.zero);

        }

        public override void OnUpdate() {
            if (playerMovementInputWrapper.WasInteractPressed()) {
                playerRaccoonComponentContainer.PlayerStateManager.UpdateState(PlayerStateId.Movement);
            }
        }

        public override PlayerStateId StateId => PlayerStateId.Hidden;
    }
}