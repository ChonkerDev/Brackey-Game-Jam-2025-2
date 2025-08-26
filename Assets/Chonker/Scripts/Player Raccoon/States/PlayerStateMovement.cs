using Chonker.Runtime.Core.StateMachine;
using UnityEngine;

namespace Chonker.Scripts.Player_Raccoon
{
    public class PlayerStateMovement : PlayerState
    {
        private Vector2 currentMovementInput;

        public override void OnEnter() {
            
        }

        public override void OnExit() {
            
        }

        public override void OnFixedUpdate() {
            
            Vector2 targetVelocity = currentMovementInput * playerRaccoonController.maxSpeed;
            Vector2 newVel = Vector2.MoveTowards(
                playerRaccoonController.Velocity,
                targetVelocity,
                playerRaccoonController.acceleration * Time.fixedDeltaTime
            );
            playerRaccoonController.SetVelocity(newVel);
            if (currentMovementInput.sqrMagnitude > 0.01f) {
                float targetAngle = Mathf.Atan2(currentMovementInput.y, currentMovementInput.x) * Mathf.Rad2Deg - 90;
                float angle = Mathf.MoveTowardsAngle(playerRaccoonController.Rotation, targetAngle, playerRaccoonController.rotationSpeed * Time.deltaTime);
                playerRaccoonController.SetRotation(angle);
            }
        }

        public override void OnUpdate() {
            currentMovementInput = playerMovementInputWrapper.ReadMovementInput();
            if (!playerMovementInputWrapper.WasInteractPressed() ||
                !playerRaccoonComponentContainer.PlayerRaccoonInteractionDetector
                    .currentProximityInteractionResponder) return;
            ((PlayerStateHidden)
                playerRaccoonComponentContainer.PlayerStateManager.GetState(PlayerStateId
                    .Hidden)).CurrentTrashCan = (RaccoonTrashcan) playerRaccoonComponentContainer.PlayerRaccoonInteractionDetector.currentProximityInteractionResponder.proximityInteractable;
            playerRaccoonComponentContainer.PlayerRaccoonInteractionDetector.currentProximityInteractionResponder.OnInteracted(playerRaccoonComponentContainer);
            playerRaccoonComponentContainer.PlayerStateManager.UpdateState(PlayerStateId.Hidden);
        }

        public override PlayerStateId StateId => PlayerStateId.Movement;
    }
}