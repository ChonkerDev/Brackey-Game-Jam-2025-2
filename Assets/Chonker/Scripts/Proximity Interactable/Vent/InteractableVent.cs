using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class InteractableVent : ProximityInteractable
{
    [SerializeField] private InteractableVent PartnerVent;
    [SerializeField] private AudioClip _ventScamperSound;
    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        Vector2 position = PartnerVent.GetTeleportPosition(PlayerRaccoonComponentContainer);
        Vector2 direction = PartnerVent.GetTeleportDirection();
        PlayerRaccoonComponentContainer.PlayerRaccoonController.Teleport(position);
        PlayerRaccoonComponentContainer.PlayerRaccoonController.SetForward(direction);
        _audioSource.PlayOneShot(_ventScamperSound);
    }

    public Vector2 GetTeleportPosition(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        float additivePosition = PlayerRaccoonComponentContainer
            .PlayerRaccoonController.Radius * 2;
        return transform.position + -transform.up * additivePosition;
    }

    public Vector2 GetTeleportDirection() {
        return -transform.up;
    }
}
