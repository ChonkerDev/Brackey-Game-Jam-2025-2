using System;
using Chonker.Scripts.Proximity_Interactable;
using UnityEngine;

public class PlayerRaccoonInteractionDetector : MonoBehaviour
{
    private ProximityInteractionResponder currentProximityInteractionResponder;
    [HideInInspector] public PlayerRaccoonComponentContainer PlayerRaccoonComponentContainer;
    private CircleCollider2D interactionDetectorCollider;
    private int interactionLayer;

    private void Awake() {
        PlayerRaccoonComponentContainer = GetComponentInParent<PlayerRaccoonComponentContainer>();
        interactionDetectorCollider = GetComponent<CircleCollider2D>();
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

    private void Update() {
        Debug.Log(currentProximityInteractionResponder);
        if (!PlayerRaccoonComponentContainer.PlayerMovementInputWrapper.WasInteractPressed() ||
            !currentProximityInteractionResponder) return;
        currentProximityInteractionResponder.OnInteracted(this);
        currentProximityInteractionResponder = null;
    }

    public void ActivateDetection(bool active) {
        gameObject.SetActive(active);
    }
}