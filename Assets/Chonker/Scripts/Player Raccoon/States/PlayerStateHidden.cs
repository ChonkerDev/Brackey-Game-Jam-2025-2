using System.Collections;
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
            CurrentTrashCan.ExitTrashcan();
            Vector2 movementInput = playerMovementInputWrapper.ReadMovementInput();
            Vector2 trashCanPosition = CurrentTrashCan.transform.position;
            float targetDistanceFromTrashCan = CurrentTrashCan.Radius +
                                               playerRaccoonComponentContainer.PlayerRaccoonController.Radius * 2;
            Vector2 raccoonPosition = playerRaccoonComponentContainer.transform.position;
            Vector2 positionCheck = trashCanPosition + movementInput * targetDistanceFromTrashCan;
            Collider2D foundCollider = Physics2D.OverlapCircle(positionCheck,
                playerRaccoonComponentContainer.PlayerRaccoonController.Radius, GameManager.ObstacleLayerMask);
            Vector2 targetDirection;
            if (movementInput.sqrMagnitude != 0 && !foundCollider) {
                targetDirection = movementInput;
            }
            else {
                targetDirection = (raccoonPosition - trashCanPosition).normalized;
            }

            Vector2 targetPosition = trashCanPosition + targetDirection * targetDistanceFromTrashCan;
            playerRaccoonComponentContainer.PlayerRaccoonController.SetForward(targetDirection);
            playerRaccoonComponentContainer.PlayerRaccoonController.Teleport(targetPosition);
            playerRaccoonController.EnablePhysicsCollider(true);
            StartCoroutine(DelayShowModel()); // so it doesn't look like the sprite teleports for a frame
        }

        public override void OnFixedUpdate() {
            playerRaccoonController.SetVelocity(Vector2.zero);

        }

        public override void OnUpdate() {
            if (playerMovementInputWrapper.WasInteractPressed()) {
                playerRaccoonComponentContainer.PlayerStateManager.UpdateState(PlayerStateId.Movement);
            }
        }

        private IEnumerator DelayShowModel() {
            int frameDelayCount = 5;
            for (int i = 0; i < frameDelayCount; i++)
                yield return null;
            playerRaccoonComponentContainer.PlayerRaccoonView.ShowModel();
        }
        

        public override PlayerStateId StateId => PlayerStateId.Hidden;
    }
}