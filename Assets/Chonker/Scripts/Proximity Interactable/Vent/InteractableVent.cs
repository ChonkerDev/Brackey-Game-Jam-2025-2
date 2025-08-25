using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class InteractableVent : ProximityInteractable
{
    [SerializeField] private InteractableVent PartnerVent;
    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        Vector2 position = PartnerVent.GetTeleportPosition(PlayerRaccoonInteractionDetector);
        Vector2 direction = PartnerVent.GetTeleportDirection();
        PlayerRaccoonInteractionDetector.PlayerRaccoonComponentContainer.PlayerRaccoonController.Teleport(position);
        PlayerRaccoonInteractionDetector.PlayerRaccoonComponentContainer.PlayerRaccoonController.SetForward(direction);
    }

    public Vector2 GetTeleportPosition(PlayerRaccoonInteractionDetector playerRaccoonInteractionDetector) {
        float additivePosition = playerRaccoonInteractionDetector.PlayerRaccoonComponentContainer
            .PlayerRaccoonController.Radius * 2;
        return transform.position + -transform.up * additivePosition;
    }

    public Vector2 GetTeleportDirection() {
        return -transform.up;
    }
}
