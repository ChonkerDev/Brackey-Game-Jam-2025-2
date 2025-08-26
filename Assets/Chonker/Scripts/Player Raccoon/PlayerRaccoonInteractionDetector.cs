using System;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class PlayerRaccoonInteractionDetector : MonoBehaviour
{
    public ProximityInteractionResponder currentProximityInteractionResponder { get; private set; }
    [HideInInspector] public PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer;
    private int interactionLayer;

    private void Awake() {
        PlayerRaccoonComponentContainer = GetComponentInParent<PlayerRaccoonComponentContainer>();
        interactionLayer = LayerMask.NameToLayer("Interaction");
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer != interactionLayer) return;
        if (other.TryGetComponent(out ProximityInteractionResponder ProximityInteractionResponder)) {
            currentProximityInteractionResponder = ProximityInteractionResponder;
            ProximityInteractionResponder.OnProximityEnter(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer != interactionLayer) return;
        if (other.TryGetComponent(out ProximityInteractionResponder ProximityInteractionResponder)) {
            currentProximityInteractionResponder = null;
            ProximityInteractionResponder.OnProximityExit(this);
        }
    }
}