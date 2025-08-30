using System;
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
                float angle = Mathf.MoveTowardsAngle(playerRaccoonController.Rotation, targetAngle,
                    playerRaccoonController.rotationSpeed * Time.deltaTime);
                playerRaccoonController.SetRotation(angle);
            }
            
        }

        public override void OnUpdate() {
            if (playerRaccoonController.Velocity.sqrMagnitude > .1f) {
                playerRaccoonComponentContainer.PlayerRaccoonAudioWrapper.PlaySkitter();
            }
            else {
                playerRaccoonComponentContainer.PlayerRaccoonAudioWrapper.StopSkitter();
            }
            
            currentMovementInput = playerMovementInputWrapper.ReadMovementInput();
            if (!playerMovementInputWrapper.WasInteractPressed() ||
                !playerRaccoonComponentContainer.PlayerRaccoonInteractionDetector
                    .currentProximityInteractionResponder) return;
            playerRaccoonComponentContainer.PlayerRaccoonInteractionDetector
                .currentProximityInteractionResponder.OnInteracted(playerRaccoonComponentContainer);
            switch (playerRaccoonComponentContainer.PlayerRaccoonInteractionDetector
                        .currentProximityInteractionResponder
                        .proximityInteractable) {
                case RaccoonTrashcan trashcan:
                    ((PlayerStateHidden)
                        playerRaccoonComponentContainer.PlayerStateManager.GetState(PlayerStateId
                            .Hidden)).CurrentTrashCan = trashcan;

                    playerRaccoonComponentContainer.PlayerStateManager.UpdateState(PlayerStateId.Hidden);
                    break;
                case InteractableVent vent:
                    vent.TeleportToPartnerVent(playerRaccoonComponentContainer);
                    break;
                default:
                    Debug.LogError(playerRaccoonComponentContainer.PlayerRaccoonInteractionDetector
                        .currentProximityInteractionResponder
                        .proximityInteractable.GetType());
                    throw new NotImplementedException();
            }
        }

        public override PlayerStateId StateId => PlayerStateId.Movement;
    }
}