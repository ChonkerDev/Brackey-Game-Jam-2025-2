using System;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class InteractableVent : ProximityInteractable
{
    [SerializeField] private InteractableVent PartnerVent;
    [SerializeField] private AudioClip _ventScamperSound;
    [SerializeField] private TransformBob _interactionIndicator;
    private float indicatorOffset = .35f;
    private void Start() {

        _interactionIndicator.transform.up = Vector2.up;
        Vector2 baseIndicatorPosition = transform.position;
        baseIndicatorPosition += Vector2.up * indicatorOffset;
        _interactionIndicator.SetBasePosition(baseIndicatorPosition);
        _interactionIndicator.transform.up = Vector2.up;
        _interactionIndicator.gameObject.SetActive(false);
    }

    public override void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        _interactionIndicator.gameObject.SetActive(true);
        
    }

    public override void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
        _interactionIndicator.gameObject.SetActive(false);

    }

    public void TeleportToPartnerVent(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
        Vector2 position = PartnerVent.GetTeleportPosition(PlayerRaccoonComponentContainer);
        Vector2 direction = PartnerVent.GetTeleportDirection();
        PlayerRaccoonComponentContainer.PlayerRaccoonController.Teleport(position);
        PlayerRaccoonComponentContainer.PlayerRaccoonController.SetForward(direction);
    }

    public override void OnInteracted(PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer) {
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
