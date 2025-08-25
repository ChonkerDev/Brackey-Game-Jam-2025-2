using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Chonker.Scripts.Proximity_Interactable
{
    public class ProximityInteractionResponder : MonoBehaviour
    {
        private ProximityInteractable proximityInteractable;

        private void Awake() {
            proximityInteractable = GetComponentInParent<ProximityInteractable>();
        }

        public void OnProximityEnter(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
            proximityInteractable.OnProximityEnter(PlayerRaccoonInteractionDetector);
        }

        public void OnProximityExit(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
            proximityInteractable.OnProximityExit(PlayerRaccoonInteractionDetector);
        }

        public void OnInteracted(PlayerRaccoonInteractionDetector PlayerRaccoonInteractionDetector) {
            proximityInteractable.OnInteracted(PlayerRaccoonInteractionDetector);
        }
    }
}